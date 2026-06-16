using Microsoft.AspNetCore.Mvc;
using RestaurantOS.Application.DTOs.Mesa;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Domain.Enums;

namespace RestaurantOS.Web.Controllers;

[ApiController]
[Route("api/mesas")]
public class MesasController : ControllerBase
{
    private readonly IMesaService _mesaService;

    public MesasController(IMesaService mesaService)
    {
        _mesaService = mesaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MesaDto>>> GetAll()
    {
        var mesas = await _mesaService.GetAllAsync();
        return Ok(mesas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MesaDto>> GetById(int id)
    {
        var mesa = await _mesaService.GetByIdAsync(id);
        if (mesa is null) return NotFound();

        return Ok(mesa);
    }

    [HttpPost]
    public async Task<ActionResult<MesaDto>> Crear(CrearMesaDto dto)
    {
        var mesa = await _mesaService.CrearAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = mesa.Id }, mesa);
    }

    [HttpPut("{id}/estado")]
    public async Task<IActionResult> CambiarEstado(int id, [FromBody] EstadoMesa nuevoEstado)
    {
        await _mesaService.CambiarEstadoAsync(id, nuevoEstado);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mesaService.DeleteAsync(id);
        return NoContent();
    }
}