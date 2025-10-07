namespace authbackend.Entities;

public class HorasTrabajadas
{
    public int Id { get; set; } // Identificador único para las horas trabajadas
    public int UserId { get; set; } // Identificador del usuario
    public User? User { get; set; } // Navegación a la entidad User

    public int Horas { get; set; } // Cantidad de horas trabajadas
    public DateOnly Fecha { get; set; } // Fecha en que se trabajaron las horas
}
