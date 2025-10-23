using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// Controlador para ServicioMedico
[ApiController]
[Route("api/[controller]")]
public class ServicioMedicoController : ControllerBase
{
    private readonly RepositorioServicioMedico _repositorio;

    public ServicioMedicoController(RepositorioServicioMedico repositorio)
    {
        _repositorio = repositorio;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ServicioMedicoDTO>> Get()
    {
        var data = _repositorio.ListarTodos();
        return Ok(data.Select(s => new ServicioMedicoDTO
        {
            Id = s.Id,
            Descripcion = s.Descripcion,
            Costo = s.Costo
        }));
    }

    [HttpGet("{id}")]
    public ActionResult<ServicioMedicoDTO> Get(Guid id)
    {
        var s = _repositorio.ObtenerPorId(id);
        if (s == null) return NotFound();

        return Ok(new ServicioMedicoDTO
        {
            Id = s.Id,
            Descripcion = s.Descripcion,
            Costo = s.Costo
        });
    }

    [HttpPost]
    public ActionResult Post([FromBody] ServicioMedicoDTO dto)
    {
        if (dto == null) return BadRequest();

        var entity = new ServicioMedico
        {
            Id = Guid.NewGuid(),
            Descripcion = dto.Descripcion,
            Costo = dto.Costo
        };

        _repositorio.Insertar(entity);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, dto);
    }

    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] ServicioMedicoDTO dto)
    {
        var existing = _repositorio.ObtenerPorId(id);
        if (existing == null) return NotFound();

        existing.Descripcion = dto.Descripcion;
        existing.Costo = dto.Costo;

        _repositorio.Actualizar(existing);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var existing = _repositorio.ObtenerPorId(id);
        if (existing == null) return NotFound();

        _repositorio.Eliminar(x => x.Id == id);
        return NoContent();
    }
}