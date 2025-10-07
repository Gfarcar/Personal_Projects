namespace authbackend.Entities;

public class MenuItem
{
    public int Id { get; set; } // Identificador único para las horas trabajadas
    public string Title { get; set; } = string.Empty; 
    public string Icon {get; set;} = string.Empty;
    public string Href {get;set;} = string.Empty;

    public ICollection<MenuPermissionsTable> MenuPermissions { get; set; } = new List<MenuPermissionsTable>();

}
