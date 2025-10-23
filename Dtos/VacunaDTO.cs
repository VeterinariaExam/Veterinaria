public class VacunaDTO
{
    public Guid Id { get; set; }
    public required string Nombre { get; set; }
    public required DateTime FechaAplicacion { get; set; }
    public required string Lote { get; set; }
}
