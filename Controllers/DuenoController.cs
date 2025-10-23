using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class DuenoController : ControllerBase
{
    private readonly RepositorioDueno _repositorio;

    public DuenoController(RepositorioDueno repositorio)
    {
        _repositorio = repositorio;
    }

    // GET api/dueno
    [HttpGet]
    public ActionResult<IEnumerable<DuenoDTO>> Get()
    {
        var duenos = _repositorio.ListarTodos();
        var dtos = duenos.Select(d => new DuenoDTO
        {
            Id = d.Id,
            Nombre = d.Nombre,
            Apellido = d.Apellido,
            Telefono = d.Telefono,
            Direccion = d.Direccion,
            MascotasIds = d.Mascotas.Select(m => m.Id).ToList()
        });
        return Ok(dtos);
    }

    // GET api/dueno/{id}
    [HttpGet("{id}")]
    public ActionResult<DuenoDTO> Get(Guid id)
    {
        var d = _repositorio.ObtenerPorId(id);
        if (d == null)
            return NotFound();

        var dto = new DuenoDTO
        {
            Id = d.Id,
            Nombre = d.Nombre,
            Apellido = d.Apellido,
            Telefono = d.Telefono,
            Direccion = d.Direccion,
            MascotasIds = d.Mascotas.Select(m => m.Id).ToList()
        };

        return Ok(dto);
    }

    // POST api/dueno
    [HttpPost]
    public ActionResult Post([FromBody] DuenoDTO dto)
    {
        if (dto == null)
            return BadRequest();

        var dueno = new Dueno
        {
            Id = Guid.NewGuid(),
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Telefono = dto.Telefono,
            Direccion = dto.Direccion
            // Mascotas se pueden agregar por otro endpoint
        };

        _repositorio.Insertar(dueno);
        return CreatedAtAction(nameof(Get), new { id = dueno.Id }, dto);
    }

    // PUT api/dueno/{id}
    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] DuenoDTO dto)
    {
        var duenoExistente = _repositorio.ObtenerPorId(id);
        if (duenoExistente == null)
            return NotFound();

        duenoExistente.Nombre = dto.Nombre;
        duenoExistente.Apellido = dto.Apellido;
        duenoExistente.Telefono = dto.Telefono;
        duenoExistente.Direccion = dto.Direccion;

        _repositorio.Actualizar(duenoExistente);
        return NoContent();
    }

    // DELETE api/dueno/{id}
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var dueno = _repositorio.ObtenerPorId(id);
        if (dueno == null)
            return NotFound();

        _repositorio.Eliminar(d => d.Id == id);
        return NoContent();
    }
}
