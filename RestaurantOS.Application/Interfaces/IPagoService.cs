using RestaurantOS.Application.DTOs.Pago;
using RestaurantOS.Application.DTOs.PagoDto;

namespace RestaurantOS.Application.Interfaces;

public interface IPagoService
{
    Task<PagoDto?> GetByIdAsync(int id);
    Task<List<PagoDto>> GetAllAsync();
    Task<PagoDto> CrearAsync(CrearPagoDto dto);
}