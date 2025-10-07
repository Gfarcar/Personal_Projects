namespace authbackend.Dtos; 

public class GetUserResponseDto{
    public string UserName {get; set;} = string.Empty; 
    public string Email {get; set;} = string.Empty;
    public string Token {get; set;} = string.Empty; 
    public string RefreshToken { get; set; } = string.Empty;
    public string TemporalToken {get; set;} = string.Empty; 
}