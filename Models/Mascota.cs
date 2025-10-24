// Clase base abstracta para mascotas
public abstract class Mascota
{
    public Guid Id { get; set; } = Guid.NewGuid();      // Identificador único
    public required string Nombre { get; set; }          // Nombre de la mascota
    public DateTime FechaNacimiento { get; set; }        // Fecha de nacimiento
    public required Dueno Dueno { get; set; }             // Dueño de la mascota
    public required string Sexo { get; set; }             // Sexo
    public required string Raza { get; set; }             // Raza
    public HistorialClinico Historial { get; set; } = new HistorialClinico(); // Historial clínico asociado
    public abstract string Especie { get; }               // Propiedad abstracta para especie
}