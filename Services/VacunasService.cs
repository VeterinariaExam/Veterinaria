// Servicio para manejo de Vacunas
public class VacunaService
{
    private readonly RepositorioVacuna _repositorioVacuna;

    public VacunaService(RepositorioVacuna repositorioVacuna)
    {
        _repositorioVacuna = repositorioVacuna;
    }

    // Lista todas las vacunas
    public IEnumerable<Vacuna> Listar()
    {
        return _repositorioVacuna.ListarTodos();
    }

    // Obtiene vacuna por ID
    public Vacuna ObtenerPorId(Guid id)
    {
        return _repositorioVacuna.ObtenerPorId(id);
    }

    // Crea vacuna con nuevo GUID
    public Vacuna Crear(Vacuna dto)
    {
        dto.Id = Guid.NewGuid();
        _repositorioVacuna.Insertar(dto);
        return dto;
    }

    // Actualiza vacuna existente
    public void Actualizar(Guid id, Vacuna dto)
    {
        var existente = _repositorioVacuna.ObtenerPorId(id);
        if (existente == null) throw new KeyNotFoundException("Vacuna no encontrada");

        existente.Nombre = dto.Nombre;
        existente.FechaAplicacion = dto.FechaAplicacion;
        existente.Lote = dto.Lote;

        _repositorioVacuna.Actualizar(existente);
    }

    // Elimina vacuna por ID
    public void Eliminar(Guid id)
    {
        _repositorioVacuna.Eliminar(x => x.Id == id);
    }
}