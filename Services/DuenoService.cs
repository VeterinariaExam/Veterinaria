
// Servicio para Dueño
public class DuenoService
{
    private readonly RepositorioDueno _repositorioDueno;
    public DuenoService(RepositorioDueno repositorioDueno)
    {
        _repositorioDueno = repositorioDueno;
    }

    public IEnumerable<Dueno> Listar()
    {
        return _repositorioDueno.ListarTodos();
    }

    public Dueno ObtenerPorId(Guid id)
    {
        return _repositorioDueno.ObtenerPorId(id);
    }

    public Dueno Crear(Dueno dto)
    {
        dto.Id = Guid.NewGuid();
        _repositorioDueno.Insertar(dto);
        return dto;
    }

    public void Actualizar(Guid id, Dueno dto)
    {
        var existente = _repositorioDueno.ObtenerPorId(id);
        if (existente == null) throw new KeyNotFoundException("Dueño no encontrado");

        existente.Nombre = dto.Nombre;
        existente.Apellido = dto.Apellido;
        existente.Telefono = dto.Telefono;
        existente.Direccion = dto.Direccion;

        _repositorioDueno.Actualizar(existente);
    }

    public void Eliminar(Guid id)
    {
        _repositorioDueno.Eliminar(x => x.Id == id);
    }

    public IEnumerable<Dueno> ListarDuenoConMascotasMinimas(int minimoMascotas)
    {
        return _repositorioDueno.ListarTodos()
            .Where(d => d.Mascotas.Count > minimoMascotas);
    }


}