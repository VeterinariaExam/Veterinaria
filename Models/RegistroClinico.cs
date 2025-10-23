public class RegistroClinico
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required DateTime Fecha { get; set; }
    public required Veterinario Veterinario { get; set; }
    public required string Diagnostico { get; set; }
    public List<ServicioMedico> ServiciosRealizados { get; set; } = new List<ServicioMedico>();
    public string NotasAdicionales { get; set; }
}