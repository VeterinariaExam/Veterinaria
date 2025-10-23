public class Especialidad
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Nombre { get; set; }
    public required string Descripcion { get; set; }
}