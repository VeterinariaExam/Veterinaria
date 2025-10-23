public class DuenoDTO : PersonaDTO
{
    public required string Direccion { get; set; }
    public List<Guid> MascotasIds { get; set; } = new List<Guid>(); // Correcto, mantenido
}
