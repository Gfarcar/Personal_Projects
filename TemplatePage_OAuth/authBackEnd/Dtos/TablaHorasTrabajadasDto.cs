namespace authbackend.Dtos; 
public class TablaHorasTrabajadasDto{
    public int Id {get; set;}
    public string Names {get; set;} = string.Empty; 
    public string LastName {get; set;} = string.Empty; 
    public string SecondLastName {get; set;} = string.Empty; 
    public int Horas {get; set;}
    public DateOnly Fecha { get; set; }
}