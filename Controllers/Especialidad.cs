using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// Controlador para Especialidad
[ApiController]
[Route("api/[controller]")]
public class EspecialidadController : ControllerBase
{
    private readonly RepositorioEspecialidad _repositorio;

    public EspecialidadController(RepositorioEspecialidad repositorio)
    {
        _repositorio = repositorio;
    }

    [HttpGet]
    public ActionResult<IEnumerable<EspecialidadDTO>> Get()
    {
        var data = _repositorio.ListarTodos();
        return Ok(data.Select(e => new EspecialidadDTO
        {
            Id = e.Id,
            Nombre = e.Nombre,
            Descripcion = e.Descripcion
        }));
    }

    [HttpGet("{id}")]
    public ActionResult<EspecialidadDTO> Get(Guid id)
    {
        var e = _repositorio.ObtenerPorId(id);
        if (e == null) return NotFound();

        return Ok(new EspecialidadDTO
        {
            Id = e.Id,
            Nombre = e.Nombre,
            Descripcion = e.Descripcion
        });
    }

    [HttpPost]
    public ActionResult Post([FromBody] EspecialidadDTO dto)
    {
        if (dto == null) return BadRequest();

        var entity = new Especialidad
        {
            Id = Guid.NewGuid(),
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion
        };

        _repositorio.Insertar(entity);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, dto);
    }

    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] EspecialidadDTO dto)
    {
        var existing = _repositorio.ObtenerPorId(id);
        if (existing == null) return NotFound();

        existing.Nombre = dto.Nombre;
        existing.Descripcion = dto.Descripcion;

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