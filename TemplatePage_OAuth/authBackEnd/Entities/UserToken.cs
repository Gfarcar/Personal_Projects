namespace authbackend.Entities; 
public class UserToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public bool IsRevoked{get; set;} 
    public User User { get; set; } = null!;
}