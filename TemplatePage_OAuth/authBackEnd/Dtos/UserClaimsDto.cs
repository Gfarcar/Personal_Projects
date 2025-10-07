namespace authbackend.Dtos; 

public class UserClaimsDto{
    public int Id {get; set;}
    public int UserId {get; set;}
    public string Names { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string SecondLastName { get; set; } = string.Empty;
    public string ClaimType { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
}