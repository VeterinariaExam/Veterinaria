// Clase que representa un registro clínico de una mascota
public class RegistroClinico
{
    public Guid Id { get; set; } = Guid.NewGuid();       // ID único
    public required DateTime Fecha { get; set; }          // Fecha del registro
    public required Veterinario Veterinario { get; set; } // Veterinario responsable
    public required string Diagnostico { get; set; }       // Diagnóstico realizado

    public List<ServicioMedico> ServiciosRealizados { get; set; } = new List<ServicioMedico>(); // Servicios asociados
    public string NotasAdicionales { get; set; }  // Notas extra opcionales
}
