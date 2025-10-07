using authbackend.Dtos;
using authbackend.Entities;
using authbackend.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace authbackend.Controllers
{
    [ApiController]
    [Route("tokens")]
    public class TokensController : ControllerBase
    {
        private readonly IRepository<UserRefreshToken, int> _refreshTokenRepository;
        private readonly IRepository<UserToken, int> _userTokenRepository;
        private readonly IRepository<User, int> _userRepository;
        private readonly ItokenService _tokenService;

        public TokensController(
            IRepository<UserRefreshToken, int> refreshTokenRepository,
            IRepository<UserToken, int> userTokenRepository,
            IRepository<User, int> userRepository,
            ItokenService tokenService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userTokenRepository = userTokenRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpGet("refreshtokens")]
        public async Task<ActionResult<GetTokenRefreshDto>> GenerateNewAccessToken(TokenGenerationDto refreshToken)
        {
            var existingRefreshToken = await _refreshTokenRepository.GetQueryable()
                .FirstOrDefaultAsync(urt => urt.RefreshToken == refreshToken.RefreshToken && !urt.IsRevoked);

            if (existingRefreshToken == null)
                return BadRequest("Invalid refresh token");

            var user = await _userRepository.GetQueryable()
                .FirstOrDefaultAsync(u => u.Id == existingRefreshToken.UserId);

            if (user == null)
                return BadRequest("User not found.");

            string newAccessToken = await _tokenService.CreateToken(user);
            string newRefreshTokenValue = _tokenService.GenerateRefreshToken();
            string newTemporalToken = await _tokenService.CreateTemporalToken(user);

            var existingToken = await _userTokenRepository.GetQueryable()
                .FirstOrDefaultAsync(ut => ut.UserId == user.Id);

            if (existingToken != null)
            {
                existingToken.Token = newAccessToken;
                existingToken.Expiration = DateTime.UtcNow.AddDays(7);
                await _userTokenRepository.UpdateAsync(existingToken);
            }
            else
            {
                var userToken = new UserToken
                {
                    UserId = user.Id,
                    Token = newAccessToken,
                    Expiration = DateTime.UtcNow.AddDays(30)
                };
                await _userTokenRepository.InsertAsync(userToken);
            }

            existingRefreshToken.RefreshToken = newRefreshTokenValue;
            existingRefreshToken.Expiration = DateTime.UtcNow.AddDays(1);
            await _refreshTokenRepository.UpdateAsync(existingRefreshToken);

            var tokenGenerationResponse = new GetTokenRefreshDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshTokenValue,
                TemporalToken = newTemporalToken
            };

            await _userTokenRepository.SaveChangesAsync(); // Assuming SaveChangesAsync is implemented in the repository

            return Ok(tokenGenerationResponse);
        }

        [HttpGet("newtemporaltoken")]
        public async Task<ActionResult<GetTokenRefreshDto>> GenerateNewTemporalToken(TokenGenerationDto refreshToken)
        {
            var existingRefreshToken = await _refreshTokenRepository.GetQueryable()
                .FirstOrDefaultAsync(urt => urt.RefreshToken == refreshToken.RefreshToken && !urt.IsRevoked);

            if (existingRefreshToken == null)
                return BadRequest("Invalid refresh token");

            var user = await _userRepository.GetQueryable()
                .FirstOrDefaultAsync(u => u.Id == existingRefreshToken.UserId);

            if (user == null)
                return BadRequest("User not found.");

            string newTemporalToken = await _tokenService.CreateTemporalToken(user);

            return Ok(new GetTokenRefreshDto
            {
                RefreshToken = newTemporalToken
            });
        }

        [HttpPost("revokeaccesstoken")]
        public async Task<ActionResult<IEnumerable<string>>> RevokeAccessToken(TokenGenerationDto refreshToken)
        {
            var existingToken = await _userTokenRepository.GetQueryable()
                .FirstOrDefaultAsync(urt => urt.Token == refreshToken.RefreshToken);

            if (existingToken == null)
            {
                return NotFound("Access token not found.");
            }

            existingToken.IsRevoked = true;

            await _userTokenRepository.SaveChangesAsync();

            return Ok(new List<string> { "Access token revoked successfully." });
        }

        [HttpPost("revokerefreshtoken")]
        public async Task<ActionResult<IEnumerable<string>>> RevokeRefreshToken(TokenGenerationDto refreshToken)
        {
            var existingRefreshToken = await _refreshTokenRepository.GetQueryable()
                .FirstOrDefaultAsync(urt => urt.RefreshToken == refreshToken.RefreshToken);

            if (existingRefreshToken == null)
            {
                return NotFound("Refresh token not found.");
            }

            existingRefreshToken.IsRevoked = true;

            await _refreshTokenRepository.SaveChangesAsync();

            return Ok(new List<string> { "Refresh token revoked successfully." });
        }
    }
}
