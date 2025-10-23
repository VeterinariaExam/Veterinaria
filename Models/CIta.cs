public class Cita
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime FechaHora { get; set; }
    public Veterinario Veterinario { get; set; }
    public string Motivo { get; set; }
    public string Estado { get; set; }
}