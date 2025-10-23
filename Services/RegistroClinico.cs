using System;
using System.Collections.Generic;
using System.Linq;

// Servicio para Registro Clínico
public class RegistroClinicoService
{
    private readonly RepositorioRegistroClinico _repositorioRegistroClinico;
    private readonly RepositorioVeterinario _repositorioVeterinario;
    private readonly RepositorioServicioMedico _repositorioServicioMedico;

    public RegistroClinicoService(
        RepositorioRegistroClinico repositorioRegistroClinico,
        RepositorioVeterinario repositorioVeterinario,
        RepositorioServicioMedico repositorioServicioMedico)
    {
        _repositorioRegistroClinico = repositorioRegistroClinico;
        _repositorioVeterinario = repositorioVeterinario;
        _repositorioServicioMedico = repositorioServicioMedico;
    }

    public IEnumerable<RegistroClinico> Listar()
    {
        return _repositorioRegistroClinico.ListarTodos();
    }

    public RegistroClinico ObtenerPorId(Guid id)
    {
        return _repositorioRegistroClinico.ObtenerPorId(id);
    }

    public RegistroClinico Crear(RegistroClinicoDTO dto)
    {
        var vet = _repositorioVeterinario.ObtenerPorId(dto.IdVeterinario)
            ?? throw new ArgumentException("Veterinario no encontrado");

        var servicios = dto.ServiciosRealizados
            .Select(id => _repositorioServicioMedico.ObtenerPorId(id))
            .Where(s => s != null)
            .ToList();

        var entity = new RegistroClinico
        {
            Id = Guid.NewGuid(),
            Fecha = dto.Fecha,
            Diagnostico = dto.Diagnostico,
            Veterinario = vet,
            ServiciosRealizados = servicios,
            NotasAdicionales = dto.NotasAdicionales
        };

        _repositorioRegistroClinico.Insertar(entity);
        return entity;
    }

    public void Actualizar(Guid id, RegistroClinicoDTO dto)
    {
        var existing = _repositorioRegistroClinico.ObtenerPorId(id)
            ?? throw new KeyNotFoundException("Registro Clínico no encontrado");

        var vet = _repositorioVeterinario.ObtenerPorId(dto.IdVeterinario)
            ?? throw new ArgumentException("Veterinario no encontrado");

        var servicios = dto.ServiciosRealizados
            .Select(sid => _repositorioServicioMedico.ObtenerPorId(sid))
            .Where(s => s != null)
            .ToList();

        existing.Fecha = dto.Fecha;
        existing.Diagnostico = dto.Diagnostico;
        existing.Veterinario = vet;
        existing.ServiciosRealizados = servicios;
        existing.NotasAdicionales = dto.NotasAdicionales;

        _repositorioRegistroClinico.Actualizar(existing);
    }

    public void Eliminar(Guid id)
    {
        _repositorioRegistroClinico.Eliminar(x => x.Id == id);
    }
}