using authbackend.Interface;
using authbackend.Entities;
using authbackend.Data;
using Microsoft.EntityFrameworkCore;

namespace authbackend.Service;
public class RoleClaimAssignService : IRoleClaimAssignService{

    private readonly UserContext _dbContext;

    public RoleClaimAssignService(UserContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task AssignRoleToUser(int userId, int roleId){
        var user = await _dbContext.Users.FindAsync(userId);
        var role = await _dbContext.Roles.FindAsync(roleId);
        var roleClaims = await _dbContext.RoleClaim.Where(rc => rc.RoleId == roleId).ToListAsync(); 


        if (user == null || role == null || roleClaims == null){
            throw new Exception("User, role or role claims not found.");
        }

        foreach(var RoleClaim in roleClaims){
            var UserClaim = new UserClaim
            {
                UserId = userId, 
                ClaimType = RoleClaim.ClaimType,
                ClaimValue = RoleClaim.ClaimValue 
            }; 
            _dbContext.UserClaims.Add(UserClaim); 
        }

        user.UserRoles.Add(new UserRole { UserId = userId, RoleId = roleId });
        await _dbContext.SaveChangesAsync();
    }

    public async Task AssignClaimToUser(int userId, string claimType, string claimValue){
        var user = await _dbContext.Users.FindAsync(userId);
    
        if (user == null){
            throw new Exception("User not found.");
        }

        user.UserClaims.Add(new UserClaim { UserId = userId, ClaimType = claimType, ClaimValue = claimValue });
        await _dbContext.SaveChangesAsync();
    }

}