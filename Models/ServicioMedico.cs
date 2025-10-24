
// Clase que representa un servicio médico veterinario
public class ServicioMedico
{
    public Guid Id { get; set; } = Guid.NewGuid();      // ID único
    public required string Descripcion { get; set; }   // Descripción del servicio
    public string detalles { get; set; }                // Detalles adicionales
    public decimal Costo { get; set; }                   // Costo del servicio
}