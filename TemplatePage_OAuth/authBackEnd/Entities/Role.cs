namespace authbackend.Entities; 

public class Role
{
    public int Id {get;set;}
    public string Name{get;set;} = string.Empty;

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<RoleClaim> RoleClaim {get;set;} = new List<RoleClaim>(); 
    public ICollection<MenuPermissionsTable> MenuPermissions { get; set; } = new List<MenuPermissionsTable>();
    
}