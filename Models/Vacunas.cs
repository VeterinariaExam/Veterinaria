public class Vacuna
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Nombre { get; set; }
    public required DateTime FechaAplicacion { get; set; }
    public  DateTime FechaVencimiento => FechaAplicacion.AddYears(1);
    public required string Lote { get; set; }
}