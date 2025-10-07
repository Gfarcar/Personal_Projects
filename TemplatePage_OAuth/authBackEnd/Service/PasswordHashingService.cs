using authbackend.Interface;

namespace authbackend.Service;

public class PasswordHashingService : IPasswordHashingService{
    public string HashPassword(string password){
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}