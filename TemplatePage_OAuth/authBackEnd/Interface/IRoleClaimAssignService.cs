using authbackend.Entities;

namespace authbackend.Interface; 


public interface IRoleClaimAssignService{
    Task AssignRoleToUser(int userId, int roleId);
    Task AssignClaimToUser(int userId, string claimType, string claimValue); 
}