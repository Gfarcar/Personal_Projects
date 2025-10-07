namespace authbackend.Dtos; 

public class UserRolesDto{
    public string Id {get; set;}  = string.Empty;
    public int UserId {get; set;}
    public string Names { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string SecondLastName { get; set; } = string.Empty;
    public int RoleId { get; set; } 
    public string RoleName { get; set; } = string.Empty;
}