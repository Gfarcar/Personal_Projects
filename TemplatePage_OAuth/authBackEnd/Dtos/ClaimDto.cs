using System.ComponentModel.DataAnnotations;
namespace authbackend.Dtos; 
public class ClaimDto
{
    [Required]
    public string Type { get; set; } = string.Empty;

    [Required]
    public string Value { get; set; } = string.Empty;
}
