using System.ComponentModel;

namespace authbackend.Dtos; 


public class TablaRoleClaimDto{
    public string Id {get;set;} = string.Empty; 
    public int RoleId {get;set;} 
    public string Role {get;set;} = string.Empty; 
    public string ClaimType {get;set;} = string.Empty; 
    public string ClaimValue { get; set; } = string.Empty; // Valor del claim
}