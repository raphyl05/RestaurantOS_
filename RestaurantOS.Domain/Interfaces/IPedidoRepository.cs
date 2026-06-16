using RestaurantOS.Domain.Entities;

namespace RestaurantOS.Domain.Interfaces;

public interface IPedidoRepository
{
    Task<Pedido?> GetByIdAsync(int id);
    Task<List<Pedido>> GetAllAsync();
    Task AddAsync(Pedido pedido);
    void Update(Pedido pedido);
    void Delete(Pedido pedido);
}