namespace authbackend.Dtos; 

public class SalarioDto{
    public int Id {get; set;}
    public decimal Monto  {get; set;}
    public DateOnly Fecha {get; set;}
    public int UserId { get; set; }
    public string Names { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string SecondLastName { get; set; } = string.Empty;
}