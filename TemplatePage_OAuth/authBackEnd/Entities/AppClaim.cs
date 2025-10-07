namespace authbackend.Entities; 

public class AppClaim
{
    public int Id {get; set;}
    public string Type {get; set;} = string.Empty;
    public string Value {get;set;} = string.Empty;
    public ICollection<RoleClaim> RoleClaims {get;set;} = new List<RoleClaim>();

}