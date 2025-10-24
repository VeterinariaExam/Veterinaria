// Clase que representa una vacuna aplicada a una mascota
public class Vacuna
{
    public Guid Id { get; set; } = Guid.NewGuid();        // Identificador único
    public required string Nombre { get; set; }           // Nombre de la vacuna
    public required DateTime FechaAplicacion { get; set; } // Fecha en que se aplicó la vacuna
    public DateTime FechaVencimiento => FechaAplicacion.AddYears(1); // Fecha de vencimiento calculada automáticamente
    public required string Lote { get; set; }             // Lote de la vacuna
}