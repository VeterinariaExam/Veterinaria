using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// Controlador para manejo de vacunas (CRUD)
[ApiController]
[Route("api/[controller]")]
public class VacunaController : ControllerBase
{
    private readonly RepositorioVacuna _repositorio;

    public VacunaController(RepositorioVacuna repositorio) { _repositorio = repositorio; }

    // Lista todas las vacunas
    [HttpGet]
    public ActionResult<IEnumerable<VacunaDTO>> Get()
    {
        var vacunas = _repositorio.ListarTodos();
        return Ok(vacunas.Select(v => new VacunaDTO
        {
            Id = v.Id,
            Nombre = v.Nombre,
            FechaAplicacion = v.FechaAplicacion,
            Lote = v.Lote
        }));
    }

    // Obtiene vacuna por ID
    [HttpGet("{id}")]
    public ActionResult<VacunaDTO> Get(Guid id)
    {
        var v = _repositorio.ObtenerPorId(id);
        if (v == null) return NotFound();
        return Ok(new VacunaDTO
        {
            Id = v.Id,
            Nombre = v.Nombre,
            FechaAplicacion = v.FechaAplicacion,
            Lote = v.Lote
        });
    }

    // Crea una nueva vacuna
    [HttpPost]
    public ActionResult Post([FromBody] VacunaDTO dto)
    {
        if (dto == null) return BadRequest();
        var vacuna = new Vacuna
        {
            Id = Guid.NewGuid(),
            Nombre = dto.Nombre,
            FechaAplicacion = dto.FechaAplicacion,
            Lote = dto.Lote
        };
        _repositorio.Insertar(vacuna);
        return CreatedAtAction(nameof(Get), new { id = vacuna.Id }, dto);
    }

    // Actualiza vacuna existente
    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] VacunaDTO dto)
    {
        var existente = _repositorio.ObtenerPorId(id);
        if (existente == null) return NotFound();

        existente.Nombre = dto.Nombre;
        existente.FechaAplicacion = dto.FechaAplicacion;
        existente.Lote = dto.Lote;

        _repositorio.Actualizar(existente);
        return NoContent();
    }

    // Elimina vacuna por ID
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var vacuna = _repositorio.ObtenerPorId(id);
        if (vacuna == null) return NotFound();
        _repositorio.Eliminar(x => x.Id == id);
        return NoContent();
    }
}