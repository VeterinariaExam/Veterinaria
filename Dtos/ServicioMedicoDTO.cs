public class ServicioMedicoDTO
{
    public Guid Id { get; set; }
    public required string Descripcion { get; set; }
    public string detalles { get; set; }
    public decimal Costo { get; set; }
}