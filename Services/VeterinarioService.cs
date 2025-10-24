// Servicio para manejo de Veterinarios
public class VeterinarioService
{
    private readonly RepositorioVeterinario _repositorioVeterinario;
    private readonly RepositorioEspecialidad _repositorioEspecialidad;
    private readonly RepositorioServicioMedico _repositorioServicioMedico;
    private readonly RepositorioMascota _repositorioMascota;



    public VeterinarioService(
    RepositorioVeterinario repositorioVeterinario,
    RepositorioEspecialidad repositorioEspecialidad,
    RepositorioServicioMedico repositorioServicioMedico,
    RepositorioMascota repositorioMascota)
    {
        _repositorioVeterinario = repositorioVeterinario;
        _repositorioEspecialidad = repositorioEspecialidad;
        _repositorioServicioMedico = repositorioServicioMedico;
        _repositorioMascota = repositorioMascota;
    }

    // Lista todos los veterinarios
    public IEnumerable<Veterinario> Listar()
    {
        return _repositorioVeterinario.ListarTodos();
    }

    // Obtiene veterinario por ID
    public Veterinario ObtenerPorId(Guid id)
    {
        return _repositorioVeterinario.ObtenerPorId(id);
    }

    // Crea nuevo veterinario, asignando especialidades y servicios brindados
    public Veterinario Crear(VeterinarioDTO dto)
    {
        var especialidades = dto.EspecialidadesIds
            .Select(id => _repositorioEspecialidad.ObtenerPorId(id))
            .Where(e => e != null)
            .ToList();

        var servicios = dto.ServiciosBrindadosIds
            .Select(id => _repositorioServicioMedico.ObtenerPorId(id))
            .Where(s => s != null)
            .ToList();

        var vet = new Veterinario
        {
            Id = Guid.NewGuid(),
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Telefono = dto.Telefono,
            Matricula = dto.Matricula,
            Especialidades = especialidades,
            ServiciosBrindados = servicios
        };

        _repositorioVeterinario.Insertar(vet);
        return vet;
    }

    // Actualiza veterinario existente con nuevos datos, especialidades y servicios
    public void Actualizar(Guid id, VeterinarioDTO dto)
    {
        var existente = _repositorioVeterinario.ObtenerPorId(id)
            ?? throw new KeyNotFoundException("Veterinario no encontrado");

        var especialidades = dto.EspecialidadesIds
            .Select(eid => _repositorioEspecialidad.ObtenerPorId(eid))
            .Where(e => e != null)
            .ToList();

        var servicios = dto.ServiciosBrindadosIds
            .Select(sid => _repositorioServicioMedico.ObtenerPorId(sid))
            .Where(s => s != null)
            .ToList();

        existente.Nombre = dto.Nombre;
        existente.Apellido = dto.Apellido;
        existente.Telefono = dto.Telefono;
        existente.Matricula = dto.Matricula;
        existente.Especialidades = especialidades;
        existente.ServiciosBrindados = servicios;

        _repositorioVeterinario.Actualizar(existente);
    }

    // Elimina veterinario por ID
    public void Eliminar(Guid id)
    {
        _repositorioVeterinario.Eliminar(x => x.Id == id);
    }

}
