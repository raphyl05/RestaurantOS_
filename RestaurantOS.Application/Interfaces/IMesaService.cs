using RestaurantOS.Application.DTOs.Mesa;
using RestaurantOS.Domain.Enums;

namespace RestaurantOS.Application.Interfaces;

public interface IMesaService
{
    Task<MesaDto?> GetByIdAsync(int id);
    Task<List<MesaDto>> GetAllAsync();
    Task<MesaDto> CrearAsync(CrearMesaDto dto);
    Task CambiarEstadoAsync(int id, EstadoMesa nuevoEstado);
    Task DeleteAsync(int id);
}