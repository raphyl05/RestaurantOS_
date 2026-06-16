using RestaurantOS.Domain.Entities;
using RestaurantOS.Domain.Enums;

namespace RestaurantOS.Domain.Interfaces;

public interface IPedidoRepository
{
    Task<Pedido?> GetByIdAsync(int id);
    Task<List<Pedido>> GetAllAsync();
    Task<List<Pedido>> GetByEstadoAsync(EstadoPedido estado);
    Task<List<Pedido>> GetByMesaIdAsync(int mesaId);
    Task AddAsync(Pedido pedido);
    void Update(Pedido pedido);
    void Delete(Pedido pedido);
}