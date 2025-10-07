namespace authbackend.Dtos; 

public class MenuItemDto
{
    public string Id { get; set; } = string.Empty;  // Identificador único para las horas trabajadas
    public string Title { get; set; } = string.Empty; 
 //   public string Icon {get; set;} = string.Empty;
    public string Href {get;set;} = string.Empty;
}