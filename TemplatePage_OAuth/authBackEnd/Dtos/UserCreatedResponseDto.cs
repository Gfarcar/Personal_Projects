namespace authbackend.Dtos; 

public class UserCreatedResponseDto{
    public string AccessToken = string.Empty;     
    public string RefreshToken = string.Empty;
    public string UserName {get; set;} = string.Empty; 
    public string[] Roles { get; set; } = Array.Empty<string>();
    public ClaimDto[] Claims { get; set; } = Array.Empty<ClaimDto>();
}