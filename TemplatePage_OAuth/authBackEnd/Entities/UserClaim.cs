namespace authbackend.Entities; 


public class UserClaim
{
    public int Id { get; set; } // Clave primaria
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public string ClaimType { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
}
