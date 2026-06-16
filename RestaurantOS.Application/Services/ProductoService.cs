using RestaurantOS.Application.DTOs.Producto;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Domain.Entities;
using RestaurantOS.Domain.Interfaces;

namespace RestaurantOS.Application.Services;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _productoRepository;

    public ProductoService(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task<ProductoDto?> GetByIdAsync(int id)
    {
        var producto = await _productoRepository.GetByIdAsync(id);
        if (producto is null) return null;

        return MapToDto(producto);
    }

    public async Task<List<ProductoDto>> GetAllAsync()
    {
        var productos = await _productoRepository.GetAllAsync();
        return productos.Select(MapToDto).ToList();
    }

    public async Task<ProductoDto> CrearAsync(CrearProductoDto dto)
    {
        var producto = new Producto
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Precio = dto.Precio,
            Categoria = dto.Categoria,
            Disponible = true
        };

        await _productoRepository.AddAsync(producto);

        return MapToDto(producto);
    }

    public async Task CambiarDisponibilidadAsync(int id, bool disponible)
    {
        var producto = await _productoRepository.GetByIdAsync(id);
        if (producto is null)
            throw new KeyNotFoundException($"Producto {id} no encontrado.");

        producto.Disponible = disponible;
        _productoRepository.Update(producto);
    }

    public async Task DeleteAsync(int id)
    {
        var producto = await _productoRepository.GetByIdAsync(id);
        if (producto is null)
            throw new KeyNotFoundException($"Producto {id} no encontrado.");

        _productoRepository.Delete(producto);
    }

    private static ProductoDto MapToDto(Producto producto)
    {
        return new ProductoDto
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio,
            Disponible = producto.Disponible,
            Categoria = producto.Categoria
        };
    }
}