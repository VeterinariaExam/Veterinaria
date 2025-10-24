// Servicio para manejo de citas veterinarias
public class CitaService
{
    // Dependencias para acceso a datos de mascotas y veterinarios
    private readonly RepositorioMascota _repositorioMascota;
    private readonly RepositorioVeterinario _repositorioVeterinario;

    // Constructor con inyección de dependencias
    public CitaService(RepositorioMascota repositorioMascota,
                       RepositorioVeterinario repositorioVeterinario)
    {
        _repositorioMascota = repositorioMascota;
        _repositorioVeterinario = repositorioVeterinario;
    }

    // Agrega una nueva cita a la mascota especificada
    public void AgregarCita(Guid idMascota, CitaDTO dto)
    {
        // Obtiene la mascota y el veterinario, verifica que existan
        var mascota = _repositorioMascota.ObtenerPorId(idMascota)
            ?? throw new ArgumentException("Mascota no encontrada");

        var vet = _repositorioVeterinario.ObtenerPorId(dto.VeterinarioId)
            ?? throw new ArgumentException("Veterinario no encontrado");

        // Crea nueva cita y la agrega al historial de la mascota
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

    // Lista todas las citas de una mascota
    public IEnumerable<Cita> ListarCitasPorMascota(Guid idMascota)
    {
        var mascota = _repositorioMascota.ObtenerPorId(idMascota)
            ?? throw new ArgumentException("Mascota no encontrada");
        return mascota.Historial.Citas;
    }

    // Obtiene una cita específica por mascota e ID de la cita
    public Cita ObtenerCitaPorMascotaYId(Guid idMascota, Guid idCita)
    {
        var mascota = _repositorioMascota.ObtenerPorId(idMascota);
        if (mascota == null) return null;
        return mascota.Historial.Citas.FirstOrDefault(c => c.Id == idCita);
    }

    // Actualiza los datos de una cita específica
    public void ActualizarCita(Guid idMascota, Guid idCita, CitaDTO dto)
    {
        var mascota = _repositorioMascota.ObtenerPorId(idMascota)
            ?? throw new ArgumentException("Mascota no encontrada");

        var cita = mascota.Historial.Citas.FirstOrDefault(c => c.Id == idCita)
            ?? throw new ArgumentException("Cita no encontrada");

        var vet = _repositorioVeterinario.ObtenerPorId(dto.VeterinarioId)
            ?? throw new ArgumentException("Veterinario no encontrado");

        // Actualiza datos de la cita
        cita.FechaHora = dto.FechaHora;
        cita.Veterinario = vet;
        cita.Motivo = dto.Motivo;
        cita.Estado = dto.Estado;

        _repositorioMascota.Actualizar(mascota);
    }

    // Elimina una cita específica de una mascota
    public void EliminarCita(Guid idMascota, Guid idCita)
    {
        var mascota = _repositorioMascota.ObtenerPorId(idMascota)
            ?? throw new ArgumentException("Mascota no encontrada");

        var cita = mascota.Historial.Citas.FirstOrDefault(c => c.Id == idCita)
            ?? throw new ArgumentException("Cita no encontrada");

        mascota.Historial.Citas.Remove(cita);
        _repositorioMascota.Actualizar(mascota);
    }

    // Obtiene veterinarios que atendieron más citas en un mes y año dados
    public IEnumerable<(Veterinario veterinario, int cantidadCitas)> ObtenerVeterinariosMasAtendieron(int año, int mes)
    {
        var mascotas = _repositorioMascota.ListarTodos();

        var citasEnMes = mascotas
            .SelectMany(m => m.Historial.Citas)
            .Where(c => c.FechaHora.Year == año && c.FechaHora.Month == mes);

        // Agrupa por veterinario y ordena por cantidad de citas descendente
        var resultado = citasEnMes
            .GroupBy(c => c.Veterinario)
            .Select(g => (veterinario: g.Key, cantidadCitas: g.Count()))
            .OrderByDescending(x => x.cantidadCitas);

        return resultado;
    }
}