using VeterinariaBDOO_SegundoParcial_NombreDeUnIntegrante.Modelo;

public class Veterinario : Persona
{
    public required string Matricula { get; set; }
    public List<Especialidad> Especialidades { get; set; } = new List<Especialidad>();
    public List<ServicioMedico> ServiciosBrindados { get; set; } = new List<ServicioMedico>();
}
