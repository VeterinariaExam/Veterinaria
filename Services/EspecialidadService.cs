// Servicio para manejo de Especialidades veterinarias
public class EspecialidadService
{
    private readonly RepositorioEspecialidad _repositorioEspecialidad;

    public EspecialidadService(RepositorioEspecialidad repositorioEspecialidad)
    {
        _repositorioEspecialidad = repositorioEspecialidad;
    }

    // Lista todas las especialidades
    public IEnumerable<Especialidad> Listar()
    {
        return _repositorioEspecialidad.ListarTodos();
    }

    // Obtiene especialidad por ID
    public Especialidad ObtenerPorId(Guid id)
    {
        return _repositorioEspecialidad.ObtenerPorId(id);
    }

    // Crea una nueva especialidad con nuevo GUID
    public Especialidad Crear(Especialidad dto)
    {
        dto.Id = Guid.NewGuid();
        _repositorioEspecialidad.Insertar(dto);
        return dto;
    }

    // Actualiza una especialidad existente
    public void Actualizar(Guid id, Especialidad dto)
    {
        var existente = _repositorioEspecialidad.ObtenerPorId(id);
        if (existente == null) throw new KeyNotFoundException("Especialidad no encontrada");

        existente.Nombre = dto.Nombre;
        existente.Descripcion = dto.Descripcion;

        _repositorioEspecialidad.Actualizar(existente);
    }

    // Elimina una especialidad por ID
    public void Eliminar(Guid id)
    {
        _repositorioEspecialidad.Eliminar(x => x.Id == id);
    }
}