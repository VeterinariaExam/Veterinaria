public class VeterinarioDTO : PersonaDTO
{
    public List<Guid> EspecialidadesIds { get; set; } = new List<Guid>(); // Cambiado a lista de Ids para especialidades
}