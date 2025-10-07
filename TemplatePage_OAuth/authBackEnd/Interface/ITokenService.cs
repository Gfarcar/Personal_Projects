using authbackend.Entities;

namespace authbackend.Interface; 


public interface ItokenService{
    Task<string> CreateToken(User user); 
    string GenerateRefreshToken(); 
    Task RemoveExpiredTokens(); 
    Task<string> CreateTemporalToken(User user); 
}