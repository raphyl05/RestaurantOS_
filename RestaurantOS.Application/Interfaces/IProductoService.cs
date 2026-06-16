using RestaurantOS.Application.DTOs.Producto;

namespace RestaurantOS.Application.Interfaces;

public interface IProductoService
{
    Task<ProductoDto?> GetByIdAsync(int id);
    Task<List<ProductoDto>> GetAllAsync();
    Task<ProductoDto> CrearAsync(CrearProductoDto dto);
    Task CambiarDisponibilidadAsync(int id, bool disponible);
    Task DeleteAsync(int id);
}