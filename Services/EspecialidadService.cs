// Servicio para Especialidad
public class EspecialidadService
{
    private readonly RepositorioEspecialidad _repositorioEspecialidad;

    public EspecialidadService(RepositorioEspecialidad repositorioEspecialidad)
    {
        _repositorioEspecialidad = repositorioEspecialidad;
    }

    public IEnumerable<Especialidad> Listar()
    {
        return _repositorioEspecialidad.ListarTodos();
    }

    public Especialidad ObtenerPorId(Guid id)
    {
        return _repositorioEspecialidad.ObtenerPorId(id);
    }

    public Especialidad Crear(Especialidad dto)
    {
        dto.Id = Guid.NewGuid();
        _repositorioEspecialidad.Insertar(dto);
        return dto;
    }

    public void Actualizar(Guid id, Especialidad dto)
    {
        var existente = _repositorioEspecialidad.ObtenerPorId(id);
        if (existente == null) throw new KeyNotFoundException("Especialidad no encontrada");

        existente.Nombre = dto.Nombre;
        existente.Descripcion = dto.Descripcion;

        _repositorioEspecialidad.Actualizar(existente);
    }

    public void Eliminar(Guid id)
    {
        _repositorioEspecialidad.Eliminar(x => x.Id == id);
    }
}
