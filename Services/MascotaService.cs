using System;
using System.Collections.Generic;
using System.Linq;

public class MascotaService
{
    private readonly RepositorioMascota _repositorioMascota;
    private readonly RepositorioDueno _repositorioDueno;
    private readonly RepositorioVeterinario _repositorioVeterinario;

    public MascotaService(RepositorioMascota repositorioMascota, RepositorioDueno repositorioDueno,RepositorioVeterinario repositorioVeterinario )
    {
        _repositorioMascota = repositorioMascota;
        _repositorioDueno = repositorioDueno;
        _repositorioVeterinario = repositorioVeterinario;
    }

    public IEnumerable<Mascota> Listar()
    {
        return _repositorioMascota.ListarTodos();
    }

    public Mascota ObtenerPorId(Guid id)
    {
        return _repositorioMascota.ObtenerPorId(id);
    }

    public Mascota Crear(MascotaDTO dto)
    {
        var dueno = _repositorioDueno.ObtenerPorId(dto.IdDueno)
            ?? throw new ArgumentException("Dueño no encontrado.");

        Mascota mascota = dto.Especie.ToLower() switch
        {
            "perro" => new Perro
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                FechaNacimiento = dto.FechaNacimiento,
                Sexo = dto.Sexo,
                Raza = dto.Raza,
                Dueno = dueno
            },
            "gato" => new Gato
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                FechaNacimiento = dto.FechaNacimiento,
                Sexo = dto.Sexo,
                Raza = dto.Raza,
                Dueno = dueno
            },
            "ave" => new Ave
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                FechaNacimiento = dto.FechaNacimiento,
                Sexo = dto.Sexo,
                Raza = dto.Raza,
                Dueno = dueno
            },
            "reptil" => new Reptil
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                FechaNacimiento = dto.FechaNacimiento,
                Sexo = dto.Sexo,
                Raza = dto.Raza,
                Dueno = dueno
            },
            _ => throw new ArgumentException("Especie no soportada")
        };

        _repositorioMascota.Insertar(mascota);
        return mascota;
    }

    public void Actualizar(Guid id, MascotaDTO dto)
    {
        var existente = _repositorioMascota.ObtenerPorId(id)
            ?? throw new KeyNotFoundException("Mascota no encontrada");

        var dueno = _repositorioDueno.ObtenerPorId(dto.IdDueno)
            ?? throw new ArgumentException("Dueño no encontrado.");

        existente.Nombre = dto.Nombre;
        existente.FechaNacimiento = dto.FechaNacimiento;
        existente.Sexo = dto.Sexo;
        existente.Raza = dto.Raza;
        existente.Dueno = dueno;

        _repositorioMascota.Actualizar(existente);
    }

    public void Eliminar(Guid id)
    {
        _repositorioMascota.Eliminar(x => x.Id == id);
    }

    public void AgregarVacunaAMascota(Guid idMascota, VacunaDTO dtoVacuna)
    {
        var mascota = _repositorioMascota.ObtenerPorId(idMascota);
        if (mascota == null)
            throw new ArgumentException("Mascota no encontrada.");

        var vacuna = new Vacuna
        {
            Id = Guid.NewGuid(),
            Nombre = dtoVacuna.Nombre,
            FechaAplicacion = dtoVacuna.FechaAplicacion,
            Lote = dtoVacuna.Lote
        };

        mascota.Historial.Vacunas.Add(vacuna);
        _repositorioMascota.Actualizar(mascota);
    }

    public IEnumerable<Mascota> ListarMascotasConVacunasVencidas()
    {
        var fechaLimite = DateTime.Now.AddYears(-1);
        return _repositorioMascota.ListarTodos()
            .Where(m => m.Historial.Vacunas.Any(v => v.FechaAplicacion <= fechaLimite));
    }

    public List<Vacuna> ListarVacunasDeMascota(Guid idMascota)
    {
        var mascota = _repositorioMascota.ObtenerPorId(idMascota)
            ?? throw new ArgumentException("Mascota no encontrada.");

        return mascota.Historial.Vacunas;
    }
    public IEnumerable<Mascota> FiltrarPorEspecie(string especie)
    {
        return _repositorioMascota.ListarTodos()
            .Where(m => m.Especie.Equals(especie, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Mascota> FiltrarPorRangoEdad(int edadMinima, int edadMaxima)
    {
        var fechaActual = DateTime.Now;
        var fechaMaximaNacimiento = fechaActual.AddYears(-edadMinima);
        var fechaMinimaNacimiento = fechaActual.AddYears(-edadMaxima - 1).AddDays(1);

        return _repositorioMascota.ListarTodos()
            .Where(m => m.FechaNacimiento >= fechaMinimaNacimiento && m.FechaNacimiento <= fechaMaximaNacimiento);
    }

    public IEnumerable<Mascota> FiltrarPorEspecieYEdadSeparados(string especie, int edadMinima, int edadMaxima)
    {
        var porEspecie = FiltrarPorEspecie(especie);
        var porEdad = FiltrarPorRangoEdad(edadMinima, edadMaxima);
        return porEspecie.Intersect(porEdad);
    }
    public HistorialClinico ObtenerHistorialCompleto(Guid idMascota)
    {
        var mascota = _repositorioMascota.ObtenerPorId(idMascota)
            ?? throw new ArgumentException("Mascota no encontrada");

        return mascota.Historial;
    }

    public void AgregarCitaAMascota(Guid idMascota, CitaDTO dtoCita)
    {
        var mascota = _repositorioMascota.ObtenerPorId(idMascota)
            ?? throw new ArgumentException("Mascota no encontrada.");

        var cita = new Cita
        {
            Id = Guid.NewGuid(),
            FechaHora = dtoCita.FechaHora,
            Veterinario = _repositorioVeterinario.ObtenerPorId(dtoCita.VeterinarioId)
                          ?? throw new ArgumentException("Veterinario no encontrado."),
            Motivo = dtoCita.Motivo,
            Estado = dtoCita.Estado
        };

        mascota.Historial.Citas.Add(cita);
        _repositorioMascota.Actualizar(mascota);
    }




}
