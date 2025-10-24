public class CitaService
{
    private readonly RepositorioMascota _repositorioMascota;
    private readonly RepositorioVeterinario _repositorioVeterinario;

    public CitaService(RepositorioMascota repositorioMascota,
                       RepositorioVeterinario repositorioVeterinario)
    {
        _repositorioMascota = repositorioMascota;
        _repositorioVeterinario = repositorioVeterinario;
    }

    public void AgregarCita(Guid idMascota, CitaDTO dto)
    {
        var mascota = _repositorioMascota.ObtenerPorId(idMascota)
            ?? throw new ArgumentException("Mascota no encontrada");

        var vet = _repositorioVeterinario.ObtenerPorId(dto.VeterinarioId)
            ?? throw new ArgumentException("Veterinario no encontrado");

        var cita = new Cita
        {
            Id = Guid.NewGuid(),
            FechaHora = dto.FechaHora,
            Veterinario = vet,
            Motivo = dto.Motivo,
            Estado = dto.Estado
        };

        mascota.Historial.Citas.Add(cita);
        _repositorioMascota.Actualizar(mascota);
    }

    public IEnumerable<Cita> ListarCitasPorMascota(Guid idMascota)
    {
        var mascota = _repositorioMascota.ObtenerPorId(idMascota)
            ?? throw new ArgumentException("Mascota no encontrada");
        return mascota.Historial.Citas;
    }

    public Cita ObtenerCitaPorMascotaYId(Guid idMascota, Guid idCita)
    {
        var mascota = _repositorioMascota.ObtenerPorId(idMascota);
        if (mascota == null) return null;
        return mascota.Historial.Citas.FirstOrDefault(c => c.Id == idCita);
    }

    public void ActualizarCita(Guid idMascota, Guid idCita, CitaDTO dto)
    {
        var mascota = _repositorioMascota.ObtenerPorId(idMascota)
            ?? throw new ArgumentException("Mascota no encontrada");

        var cita = mascota.Historial.Citas.FirstOrDefault(c => c.Id == idCita)
            ?? throw new ArgumentException("Cita no encontrada");

        var vet = _repositorioVeterinario.ObtenerPorId(dto.VeterinarioId)
            ?? throw new ArgumentException("Veterinario no encontrado");

        cita.FechaHora = dto.FechaHora;
        cita.Veterinario = vet;
        cita.Motivo = dto.Motivo;
        cita.Estado = dto.Estado;

        _repositorioMascota.Actualizar(mascota);
    }

    public void EliminarCita(Guid idMascota, Guid idCita)
    {
        var mascota = _repositorioMascota.ObtenerPorId(idMascota)
            ?? throw new ArgumentException("Mascota no encontrada");

        var cita = mascota.Historial.Citas.FirstOrDefault(c => c.Id == idCita)
            ?? throw new ArgumentException("Cita no encontrada");

        mascota.Historial.Citas.Remove(cita);
        _repositorioMascota.Actualizar(mascota);
    }
    

    public IEnumerable<(Veterinario veterinario, int cantidadCitas)> ObtenerVeterinariosMasAtendieron(int año, int mes)
    {
        var mascotas = _repositorioMascota.ListarTodos();

        var citasEnMes = mascotas
            .SelectMany(m => m.Historial.Citas)
            .Where(c => c.FechaHora.Year == año && c.FechaHora.Month == mes);

        var resultado = citasEnMes
            .GroupBy(c => c.Veterinario)
            .Select(g => (veterinario: g.Key, cantidadCitas: g.Count()))
            .OrderByDescending(x => x.cantidadCitas);

        return resultado;
    }
}