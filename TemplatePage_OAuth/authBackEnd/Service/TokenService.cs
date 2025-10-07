using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using authbackend.Entities;
using authbackend.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using authbackend.Data;
using Microsoft.EntityFrameworkCore;
using authbackend.Dtos;
using Microsoft.Extensions.Configuration.UserSecrets;


namespace authbackend.Service; 

public class TokenService : ItokenService
{
    private readonly IConfiguration _config; 
    private readonly SymmetricSecurityKey _key;
    private readonly UserContext _dbContext; 

    public TokenService(IConfiguration config, UserContext dbContext){
        _dbContext = dbContext;
        _config = config; 
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AppSettings:Token"]));

    }

        public async Task<string> CreateToken(User user){   
            var userWithRoles = await _dbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Include(u => u.UserClaims)
            .Include(u => u.UserRefreshToken)
            .FirstOrDefaultAsync(u => u.Id == user.Id); 

            var activeTokensCount = userWithRoles.UserRefreshToken.Count(t => !t.IsRevoked);          

            if(activeTokensCount >= 3)
            {
                var oldestToken = await _dbContext.UserRefreshTokens
                .Where(t => t.UserId == user.Id && !t.IsRevoked)
                .OrderBy(t => t.Expiration)
                .FirstOrDefaultAsync(); 

                if(oldestToken != null)
                {
                    _dbContext.UserRefreshTokens.Remove(oldestToken); 
                    await _dbContext.SaveChangesAsync(); 
                }
            }

            if (userWithRoles == null)
                throw new Exception("User not found");


            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Email, userWithRoles.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, userWithRoles.Names),  
            };


            foreach (var role in userWithRoles.UserRoles.Select(ur => ur.Role.Name)){
                claims.Add(new Claim(ClaimTypes.Role, role)); 
            }

            foreach(var claim in userWithRoles.UserClaims){
                claims.Add(new Claim(claim.ClaimType, claim.ClaimValue)); 
            }

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);
        var tokenDescriptor = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(claims), 
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds, 
            Issuer = _config["AppSettings:Issuer"], 
            Audience = _config["AppSettings:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler(); 

        var token = tokenHandler.CreateToken(tokenDescriptor); 

        return tokenHandler.WriteToken(token); 

    }

    public string GenerateRefreshToken(){
        return Guid.NewGuid().ToString();
    }

    public async Task<string> CreateTemporalToken(User user){
           var userWithRoles = await _dbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Include(u => u.UserClaims)
            .Include(u => u.UserRefreshToken)
            .FirstOrDefaultAsync(u => u.Id == user.Id); 
            
            var claims = new List<Claim>();

            foreach (var role in userWithRoles.UserRoles.Select(ur => ur.Role.Name)){
                claims.Add(new Claim(ClaimTypes.Role, role)); 
            }

            foreach(var claim in userWithRoles.UserClaims){
                claims.Add(new Claim(claim.ClaimType, claim.ClaimValue)); 
            }
            

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);
        var tokenDescriptor = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(claims), 
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = creds, 
            Issuer = _config["AppSettings:Issuer"], 
            Audience = _config["AppSettings:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler(); 

        var token = tokenHandler.CreateToken(tokenDescriptor); 

        return tokenHandler.WriteToken(token); 
    } 

    public async Task<bool> IsRefreshTokenValid(string token){
        var userToken = await _dbContext.UserRefreshTokens.FirstOrDefaultAsync(t => t.RefreshToken == token && t.Expiration > DateTime.UtcNow && !t.IsRevoked); 
        return userToken != null; 
    }

    public async Task<bool> IsAccessTokenValid(string token){
        var userAccessToken = await _dbContext.UserTokens.FirstOrDefaultAsync(t => t.Token == token && t.Expiration > DateTime.UtcNow && !t.IsRevoked); 
        return userAccessToken != null;
    }

    public async void RevokeRefreshToken(string token){
        var refreshToken = await _dbContext.UserRefreshTokens.FirstOrDefaultAsync(t => t.RefreshToken == token); 

        if(refreshToken != null){
            refreshToken.IsRevoked = true;
            await _dbContext.SaveChangesAsync(); 
        }
    }

    public async void RevokeAccessToken(string token){
        var accessToken = await _dbContext.UserTokens.FirstOrDefaultAsync(t => t.Token == token); 
        if(accessToken != null){
            accessToken.IsRevoked = true; 
            await _dbContext.SaveChangesAsync(); 
        }
    }


    public async Task RemoveExpiredTokens(){
        var expiredTokens = await _dbContext.UserRefreshTokens
        .Where(t => t.Expiration < DateTime.UtcNow)
        .ToListAsync(); 

        _dbContext.UserRefreshTokens.RemoveRange(expiredTokens); 
        await _dbContext.SaveChangesAsync(); 
    }


}