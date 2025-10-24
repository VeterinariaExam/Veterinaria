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

    public MascotaController(MascotaService service, MascotaService mascotaService)
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

    [HttpPost("{id}/vacunas")]
    public ActionResult AgregarVacuna(Guid id, [FromBody] VacunaDTO dtoVacuna)
    {
        if (dtoVacuna == null) return BadRequest("Vacuna inválida.");

        try
        {
            _service.AgregarVacunaAMascota(id, dtoVacuna);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("vacunas-vencidas")]
    public ActionResult<IEnumerable<MascotaDTO>> GetVacunasVencidas()
    {
        var mascotas = _service.ListarMascotasConVacunasVencidas();
        var dtos = mascotas.Select(m => new MascotaDTO
        {
            Id = m.Id,
            Nombre = m.Nombre,
            Especie = m.Especie,
            FechaNacimiento = m.FechaNacimiento,
            Sexo = m.Sexo,
            Raza = m.Raza,
            IdDueno = m.Dueno?.Id ?? Guid.Empty
        });
        return Ok(dtos);
    }

    [HttpGet("{id}/vacunas")]
    public ActionResult<IEnumerable<VacunaDTO>> GetVacunas(Guid id)
    {
        try
        {
            var vacunas = _service.ListarVacunasDeMascota(id);

            var dtos = vacunas.Select(v => new VacunaDTO
            {
                Id = v.Id,
                Nombre = v.Nombre,
                FechaAplicacion = v.FechaAplicacion,
                Lote = v.Lote
            });

            return Ok(dtos);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("filtrar")]
    public ActionResult<IEnumerable<MascotaDTO>> FiltrarMascotas([FromQuery] string? especie, [FromQuery] int? edadMinima, [FromQuery] int? edadMaxima)
    {
        IEnumerable<Mascota> resultados = _service.Listar();

        if (!string.IsNullOrEmpty(especie))
            resultados = resultados.Where(m => m.Especie.Equals(especie, StringComparison.OrdinalIgnoreCase));

        var fechaActual = DateTime.Now;

        if (edadMinima.HasValue)
        {
            var fechaMaximaNacimiento = fechaActual.AddYears(-edadMinima.Value);
            resultados = resultados.Where(m => m.FechaNacimiento <= fechaMaximaNacimiento);
        }

        if (edadMaxima.HasValue)
        {
            var fechaMinimaNacimiento = fechaActual.AddYears(-edadMaxima.Value);
            resultados = resultados.Where(m => m.FechaNacimiento >= fechaMinimaNacimiento);
        }

        var dtos = resultados.Select(m => new MascotaDTO
        {
            Id = m.Id,
            Nombre = m.Nombre,
            Especie = m.Especie,
            FechaNacimiento = m.FechaNacimiento,
            Sexo = m.Sexo,
            Raza = m.Raza,
            IdDueno = m.Dueno?.Id ?? Guid.Empty
        });

        return Ok(dtos);
    }

    [HttpGet("{id}/historial-completo")]
    public ActionResult<HistorialClinicoDTO> GetHistorialCompleto(Guid id)
    {
        try
        {
            var historial = _service.ObtenerHistorialCompleto(id);

            var dto = new HistorialClinicoDTO
            {
                Registros = historial.Registros.Select(r => new RegistroClinicoDTO
                {
                    Id = r.Id,
                    Fecha = r.Fecha,
                    IdVeterinario = r.Veterinario.Id,
                    Diagnostico = r.Diagnostico,
                    ServiciosRealizados = r.ServiciosRealizados.Select(s => s.Id).ToList(),
                    NotasAdicionales = r.NotasAdicionales
                }).ToList(),

                Vacunas = historial.Vacunas.Select(v => new VacunaDTO
                {
                    Id = v.Id,
                    Nombre = v.Nombre,
                    FechaAplicacion = v.FechaAplicacion,
                    Lote = v.Lote
                }).ToList(),

                Citas = historial.Citas.Select(c => new CitaDTO
                {
                    Id = c.Id,
                    FechaHora = c.FechaHora,
                    VeterinarioId = c.Veterinario.Id,
                    Motivo = c.Motivo,
                    Estado = c.Estado
                }).ToList()
            };

            return Ok(dto);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
