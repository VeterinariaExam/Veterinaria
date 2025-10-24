using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// Controlador para manejo de veterinarios (CRUD)
[ApiController]
[Route("api/[controller]")]
public class VeterinarioController : ControllerBase
{
    private readonly VeterinarioService _service;

    public VeterinarioController(VeterinarioService service)
    {
        _service = service;
    }

    // Lista todos los veterinarios
    [HttpGet]
    public ActionResult<IEnumerable<VeterinarioDTO>> Get()
    {
        var vets = _service.Listar();
        var dtos = vets.Select(v => new VeterinarioDTO
        {
            Id = v.Id,
            Nombre = v.Nombre,
            Apellido = v.Apellido,
            Telefono = v.Telefono,
            Matricula = v.Matricula,
            EspecialidadesIds = v.Especialidades.Select(e => e.Id).ToList(),
            ServiciosBrindadosIds = v.ServiciosBrindados.Select(s => s.Id).ToList()
        });
        return Ok(dtos);
    }

    // Obtiene veterinario por ID
    [HttpGet("{id}")]
    public ActionResult<VeterinarioDTO> Get(Guid id)
    {
        var v = _service.ObtenerPorId(id);
        if (v == null) return NotFound();

        var dto = new VeterinarioDTO
        {
            Id = v.Id,
            Nombre = v.Nombre,
            Apellido = v.Apellido,
            Telefono = v.Telefono,
            Matricula = v.Matricula,
            EspecialidadesIds = v.Especialidades.Select(e => e.Id).ToList(),
            ServiciosBrindadosIds = v.ServiciosBrindados.Select(s => s.Id).ToList()
        };
        return Ok(dto);
    }

    // Crea nuevo veterinario
    [HttpPost]
    public ActionResult Post([FromBody] VeterinarioDTO dto)
    {
        try
        {
            var vet = _service.Crear(dto);
            return CreatedAtAction(nameof(Get), new { id = vet.Id }, dto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Actualiza veterinario existente
    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] VeterinarioDTO dto)
    {
        try
        {
            _service.Actualizar(id, dto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Elimina veterinario por ID
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        _service.Eliminar(id);
        return NoContent();
    }
}