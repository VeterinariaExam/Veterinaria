using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// Controlador para registros clínicos (CRUD básico)
[ApiController]
[Route("api/[controller]")]
public class RegistroClinicoController : ControllerBase
{
    private readonly RegistroClinicoService _service;

    public RegistroClinicoController(RegistroClinicoService service)
    {
        _service = service;
    }

    // Lista todos los registros clínicos
    [HttpGet]
    public ActionResult<IEnumerable<RegistroClinico>> Get()
    {
        return Ok(_service.Listar());
    }

    // Obtiene registro clínico por ID
    [HttpGet("{id}")]
    public ActionResult<RegistroClinico> Get(Guid id)
    {
        var r = _service.ObtenerPorId(id);
        if (r == null) return NotFound();
        return Ok(r);
    }

    // Crea un nuevo registro clínico
    [HttpPost]
    public ActionResult Post([FromBody] RegistroClinicoDTO dto)
    {
        try
        {
            var registro = _service.Crear(dto);
            return CreatedAtAction(nameof(Get), new { id = registro.Id }, dto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Actualiza un registro clínico existente
    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] RegistroClinicoDTO dto)
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

    // Elimina un registro clínico por ID
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        _service.Eliminar(id);
        return NoContent();
    }
}