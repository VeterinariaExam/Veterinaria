public class RegistroClinicoDTO
{
    public Guid Id { get; set; }
    public DateTime Fecha { get; set; }
    public Guid IdVeterinario { get; set; }
    public required string Diagnostico { get; set; }
    public List<Guid> ServiciosRealizados{ get; set; } = new List<Guid>();
    public string NotasAdicionales { get; set; }
}