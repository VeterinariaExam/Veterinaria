public abstract class Mascota
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Nombre { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public required Dueno Dueno { get; set; }
    public required string Sexo { get; set; }
    public required string Raza { get; set; }
    public HistorialClinico Historial { get; set; } = new HistorialClinico();
    public abstract string Especie { get; }
}