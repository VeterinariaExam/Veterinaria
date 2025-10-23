public class VeterinarioDTO : PersonaDTO
{
    public required string Matricula { get; set; }
    public List<Guid> EspecialidadesIds { get; set; } = new List<Guid>();
    public List<Guid> ServiciosBrindadosIds { get; set; } = new List<Guid>(); // Renombrar por claridad
}