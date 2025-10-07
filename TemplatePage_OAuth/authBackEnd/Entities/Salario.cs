namespace authbackend.Entities;

public class Salario
{
    public int Id { get; set; }
    public decimal Monto { get; set; }
    public DateOnly Fecha { get; set; } 
    public int UserId { get; set; } // Relación con el usuario
    public User? User { get; set; } // Navegación a la entidad User
}
