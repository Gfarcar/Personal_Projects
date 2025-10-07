

using authbackend.Dtos;

namespace authbackend.Entities; 

public class User 
{
    public int Id {get; set;}
    public string Names {get; set;} = string.Empty; 
    public string LastName {get; set;} = string.Empty; 
    public string SecondLastName {get; set;} = string.Empty; 
    public string Email {get; set;} = string.Empty;
    public string PasswordHash {get; set;} = string.Empty; 


    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();
    public ICollection<UserToken> UserTokens { get; set; } = new List<UserToken>();
    public ICollection<UserRefreshToken> UserRefreshToken { get; set; } = new List<UserRefreshToken>();
    public ICollection<HorasTrabajadas> HorasTrabajadas { get; set; } = new List<HorasTrabajadas>();
    public ICollection<Salario> Salarios { get; set; } = new List<Salario>(); 
}