public class CitaDTO
{
    public Guid Id { get; set; }
    public DateTime FechaHora { get; set; }
    public Guid VeterinarioId { get; set; }
    public string Motivo { get; set; }
    public string Estado { get; set; }
}
