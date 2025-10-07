namespace authbackend.Entities; 


public class UserRefreshToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string RefreshToken { get; set; } = string.Empty; // Token de refresh
    public DateTime Expiration { get; set; } // Expiración del refresh token
    public bool IsRevoked {get; set;} 
    public User User { get; set; } = null!;
}
