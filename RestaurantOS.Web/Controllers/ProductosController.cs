using Microsoft.AspNetCore.Mvc;
using RestaurantOS.Application.DTOs.Producto;
using RestaurantOS.Application.Interfaces;

namespace RestaurantOS.Web.Controllers;

[ApiController]
[Route("api/productos")]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _productoService;

    public ProductosController(IProductoService productoService)
    {
        _productoService = productoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductoDto>>> GetAll()
    {
        var productos = await _productoService.GetAllAsync();
        return Ok(productos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductoDto>> GetById(int id)
    {
        var producto = await _productoService.GetByIdAsync(id);
        if (producto is null) return NotFound();

        return Ok(producto);
    }

    [HttpPost]
    public async Task<ActionResult<ProductoDto>> Crear(CrearProductoDto dto)
    {
        var producto = await _productoService.CrearAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
    }

    [HttpPut("{id}/disponibilidad")]
    public async Task<IActionResult> CambiarDisponibilidad(int id, [FromBody] bool disponible)
    {
        await _productoService.CambiarDisponibilidadAsync(id, disponible);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productoService.DeleteAsync(id);
        return NoContent();
    }
}