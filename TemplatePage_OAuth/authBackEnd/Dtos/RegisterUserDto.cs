namespace authbackend.Dtos; 
public class RegisterUserDto{
    public string Names {get; set;} = string.Empty; 
    public string LastName {get; set;} = string.Empty; 
    public string SecondLastName {get; set;} = string.Empty; 
    public string Email {get; set;} = string.Empty;
    public string Password {get; set;} = string.Empty; 
}