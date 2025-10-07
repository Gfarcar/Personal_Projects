namespace authbackend.Interface; 

public interface IPasswordHashingService{
    string HashPassword(string Password);
}