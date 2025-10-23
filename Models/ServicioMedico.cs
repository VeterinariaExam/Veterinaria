public class ServicioMedico
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Descripcion { get; set; }
    public required string detalles { get; set; }
    public decimal Costo { get; set; }
}