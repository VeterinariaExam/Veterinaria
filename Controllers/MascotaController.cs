using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

// Controlador para Mascota
[ApiController]
[Route("api/[controller]")]
public class MascotaController : ControllerBase
{
    private readonly MascotaService _service;
    private readonly MascotaService _mascotaService;

    public MascotaController(MascotaService service,MascotaService mascotaService)
    {
        _service = service;
        _mascotaService = mascotaService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<MascotaDTO>> Get()
    {
        var mascotas = _service.Listar();
        return Ok(mascotas.Select(m => new MascotaDTO
        {
            Id = m.Id,
            Nombre = m.Nombre,
            Especie = m.Especie,
            FechaNacimiento = m.FechaNacimiento,
            Sexo = m.Sexo,
            Raza = m.Raza,
            IdDueno = m.Dueno?.Id ?? Guid.Empty
        }));
    }

    [HttpGet("{id}")]
    public ActionResult<MascotaDTO> Get(Guid id)
    {
        var m = _service.ObtenerPorId(id);
        if (m == null) return NotFound();
        return Ok(new MascotaDTO
        {
            Id = m.Id,
            Nombre = m.Nombre,
            Especie = m.Especie,
            FechaNacimiento = m.FechaNacimiento,
            Sexo = m.Sexo,
            Raza = m.Raza,
            IdDueno = m.Dueno?.Id ?? Guid.Empty
        });
    }

    [HttpPost]
    public ActionResult Post([FromBody] MascotaDTO dto)
    {
        if (dto == null) return BadRequest();

        try
        {
            var mascota = _service.Crear(dto);
            return CreatedAtAction(nameof(Get), new { id = mascota.Id }, dto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] MascotaDTO dto)
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

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        _service.Eliminar(id);
        return NoContent();
    }

    [HttpGet("vacunas-vencidas")]
    public ActionResult<IEnumerable<MascotaDTO>> GetVacunasVencidas()
    {
        var mascotas = _mascotaService.ListarMascotasConVacunasVencidas();
        var dtos = mascotas.Select(m => new MascotaDTO
        {
            Id = m.Id,
            Nombre = m.Nombre,
            Especie = m.Especie,
            FechaNacimiento = m.FechaNacimiento,
            Sexo = m.Sexo,
            Raza = m.Raza,
        });
        return Ok(dtos);
    }


}
