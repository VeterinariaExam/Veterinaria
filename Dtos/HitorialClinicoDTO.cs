public class HistorialClinicoDTO
{
    public List<RegistroClinicoDTO> Registros { get; set; } = new List<RegistroClinicoDTO>();
    public List<VacunaDTO> Vacunas { get; set; } = new List<VacunaDTO>();
    public List<CitaDTO> Citas { get; set; } = new List<CitaDTO>();
}