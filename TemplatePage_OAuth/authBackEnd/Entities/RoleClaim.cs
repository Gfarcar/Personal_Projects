namespace authbackend.Entities; 

public class RoleClaim
{
    public string Key => $"{RoleId}-{ClaimType}";
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;

    public string ClaimType { get; set; } = string.Empty; // Tipo de claim
    public string ClaimValue { get; set; } = string.Empty; // Valor del claim
}
