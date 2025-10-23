public class MascotaDTO
{
    public Guid Id { get; set; }
    public required string Nombre { get; set; }
    public required string Especie { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public required string Sexo { get; set; }
    public required string Raza { get; set; } 
    public Guid IdDueno { get; set; }
}