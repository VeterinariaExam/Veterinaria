// Clase que representa un veterinario, hereda de Persona
using VeterinariaBDOO_SegundoParcial_NombreDeUnIntegrante.Modelo;

public class Veterinario : Persona
{
    public required string Matricula { get; set; }         // Matrícula profesional
    public List<Especialidad> Especialidades { get; set; } = new List<Especialidad>(); // Especialidades que posee
    public List<ServicioMedico> ServiciosBrindados { get; set; } = new List<ServicioMedico>(); // Servicios que brinda
}