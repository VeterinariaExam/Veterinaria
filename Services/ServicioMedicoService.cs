// Servicio para ServicioMedico
public class ServicioMedicoService
{
    private readonly RepositorioServicioMedico _repositorioServicioMedico;

    public ServicioMedicoService(RepositorioServicioMedico repositorioServicioMedico)
    {
        _repositorioServicioMedico = repositorioServicioMedico;
    }

    public IEnumerable<ServicioMedico> Listar()
    {
        return _repositorioServicioMedico.ListarTodos();
    }

    public ServicioMedico ObtenerPorId(Guid id)
    {
        return _repositorioServicioMedico.ObtenerPorId(id);
    }

    public ServicioMedico Crear(ServicioMedico dto)
    {
        dto.Id = Guid.NewGuid();
        _repositorioServicioMedico.Insertar(dto);
        return dto;
    }

    public void Actualizar(Guid id, ServicioMedico dto)
    {
        var existente = _repositorioServicioMedico.ObtenerPorId(id);
        if (existente == null) throw new KeyNotFoundException("Servicio médico no encontrado");

        existente.Descripcion = dto.Descripcion;
        existente.Costo = dto.Costo;

        _repositorioServicioMedico.Actualizar(existente);
    }

    public void Eliminar(Guid id)
    {
        _repositorioServicioMedico.Eliminar(x => x.Id == id);
    }
}