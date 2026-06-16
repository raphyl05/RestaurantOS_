using RestaurantOS.Domain.Entities;

namespace RestaurantOS.Domain.Interfaces;

public interface IProductoRepository
{
    Task<Producto?> GetByIdAsync(int id);
    Task<List<Producto>> GetAllAsync();
    Task AddAsync(Producto producto);
    void Update(Producto producto);
    void Delete(Producto producto);
}