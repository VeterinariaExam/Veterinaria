using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// Controlador para Dueno
[ApiController]
[Route("api/[controller]")]
public class DuenoController : ControllerBase
{
    private readonly RepositorioDueno _repositorio;
    private readonly DuenoService _duenoService;
    private readonly MascotaService _repositorioMascota;


    public DuenoController(
    RepositorioDueno repositorio,
    DuenoService duenoService,
    MascotaService mascotaService)
{
    _repositorio = repositorio;
    _duenoService = duenoService;
    _repositorioMascota = mascotaService;
}

    [HttpGet]
    public ActionResult<IEnumerable<DuenoDTO>> Get()
    {
        var data = _repositorio.ListarTodos();
        return Ok(data.Select(d => new DuenoDTO
        {
            Id = d.Id,
            Nombre = d.Nombre,
            Apellido = d.Apellido,
            Telefono = d.Telefono,
            Direccion = d.Direccion,
            MascotasIds = d.Mascotas.Select(m => m.Id).ToList()
        }));
    }

    [HttpGet("{id}")]
    public ActionResult<DuenoDTO> Get(Guid id)
    {
        var d = _repositorio.ObtenerPorId(id);
        if (d == null) return NotFound();
        return Ok(new DuenoDTO
        {
            Id = d.Id,
            Nombre = d.Nombre,
            Apellido = d.Apellido,
            Telefono = d.Telefono,
            Direccion = d.Direccion,
            MascotasIds = d.Mascotas.Select(m => m.Id).ToList()
        });
    }

    [HttpPost]
    public ActionResult Post([FromBody] DuenoDTO dto)
    {
        if (dto == null) return BadRequest();
        var entity = new Dueno
        {
            Id = Guid.NewGuid(),
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Telefono = dto.Telefono,
            Direccion = dto.Direccion
        };
        _repositorio.Insertar(entity);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, dto);
    }

    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] DuenoDTO dto)
    {
        var existing = _repositorio.ObtenerPorId(id);
        if (existing == null)
            return NotFound();

        existing.Nombre = dto.Nombre;
        existing.Apellido = dto.Apellido;
        existing.Telefono = dto.Telefono;
        existing.Direccion = dto.Direccion;

        existing.Mascotas.Clear();
        foreach (var mascotaId in dto.MascotasIds)
        {
            var mascota = _repositorioMascota.ObtenerPorId(mascotaId);
            if (mascota != null)
                existing.Mascotas.Add(mascota);
            else
                return BadRequest($"Mascota con Id {mascotaId} no existe.");
        }

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

    [HttpGet("con-mascotas-minimas/{minimo}")]
    public ActionResult<IEnumerable<DuenoDTO>> GetConMascotasMinimas(int minimo)
    {
        var duenos = _duenoService.ListarDuenoConMascotasMinimas(minimo);
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


}