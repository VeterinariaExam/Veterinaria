// Clase para agrupar el historial clínico de una mascota
public class HistorialClinico
{
    public List<RegistroClinico> Registros { get; set; } = new List<RegistroClinico>(); // Registros clínicos
    public List<Vacuna> Vacunas { get; set; } = new List<Vacuna>();                     // Vacunas aplicadas
    public List<Cita> Citas { get; set; } = new List<Cita>();                           // Citas veterinarias
}