using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class CitaController : ControllerBase
{
    private readonly CitaService _service;

    public CitaController(CitaService service)
    {
        _service = service;
    }

    [HttpPost("mascota/{idMascota}/citas")]
    public ActionResult AgregarCita(Guid idMascota, [FromBody] CitaDTO dto)
    {
        if (dto == null) return BadRequest("Cita inválida.");

        try
        {
            _service.AgregarCita(idMascota, dto);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("mascota/{idMascota}/citas")]
    public ActionResult<IEnumerable<CitaDTO>> ListarPorMascota(Guid idMascota)
    {
        try
        {
            var citas = _service.ListarCitasPorMascota(idMascota);
            var dtos = citas.Select(c => new CitaDTO
            {
                Id = c.Id,
                FechaHora = c.FechaHora,
                VeterinarioId = c.Veterinario.Id,
                Motivo = c.Motivo,
                Estado = c.Estado
            });
            return Ok(dtos);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("mascota/{idMascota}/cita/{idCita}")]
    public ActionResult<CitaDTO> ObtenerCita(Guid idMascota, Guid idCita)
    {
        var cita = _service.ObtenerCitaPorMascotaYId(idMascota, idCita);
        if (cita == null) return NotFound();

        var dto = new CitaDTO
        {
            Id = cita.Id,
            FechaHora = cita.FechaHora,
            VeterinarioId = cita.Veterinario.Id,
            Motivo = cita.Motivo,
            Estado = cita.Estado
        };
        return Ok(dto);
    }

    [HttpPut("mascota/{idMascota}/cita/{idCita}")]
    public ActionResult ActualizarCita(Guid idMascota, Guid idCita, [FromBody] CitaDTO dto)
    {
        try
        {
            _service.ActualizarCita(idMascota, idCita, dto);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("mascota/{idMascota}/cita/{idCita}")]
    public ActionResult EliminarCita(Guid idMascota, Guid idCita)
    {
        try
        {
            _service.EliminarCita(idMascota, idCita);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("veterinarios/mas-citas")]
    public ActionResult<IEnumerable<object>> VeterinariosMasCitas([FromQuery] int año, [FromQuery] int mes)
    {
        var resultado = _service.ObtenerVeterinariosMasAtendieron(año, mes)
            .Select(v => new
            {
                VeterinarioId = v.veterinario.Id,
                Nombre = v.veterinario.Nombre,
                CantidadCitas = v.cantidadCitas
            });

        return Ok(resultado);
    }
}