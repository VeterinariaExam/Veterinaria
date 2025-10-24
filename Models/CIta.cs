// Clase que representa una cita veterinaria con detalles
public class Cita
{
    public Guid Id { get; set; } = Guid.NewGuid(); // Identificador único de la cita
    public DateTime FechaHora { get; set; }        // Fecha y hora de la cita
    public Veterinario Veterinario { get; set; }   // Veterinario encargado
    public string Motivo { get; set; }              // Motivo de la cita
    public string Estado { get; set; }              // Estado actual de la cita
}