using RestaurantOS.Application.DTOs.Pedido;
using RestaurantOS.Domain.Enums;

namespace RestaurantOS.Application.Interfaces;

public interface IPedidoService
{
    Task<PedidoDto?> GetByIdAsync(int id);
    Task<List<PedidoDto>> GetAllAsync();
    Task<List<PedidoDto>> GetByEstadoAsync(EstadoPedido estado);
    Task<PedidoDto> CrearAsync(CrearPedidoDto dto);
    Task CambiarEstadoAsync(int id, EstadoPedido nuevoEstado);
}