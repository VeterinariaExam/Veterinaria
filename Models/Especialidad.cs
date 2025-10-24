// Clase que representa una especialidad veterinaria
public class Especialidad
{
    public Guid Id { get; set; } = Guid.NewGuid(); // ID único
    public required string Nombre { get; set; }    // Nombre de la especialidad
    public required string Descripcion { get; set; } // Descripción de la especialidad
}