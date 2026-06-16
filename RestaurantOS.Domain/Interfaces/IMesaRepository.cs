using RestaurantOS.Domain.Entities;

namespace RestaurantOS.Domain.Interfaces;

public interface IMesaRepository
{
    Task<Mesa?> GetByIdAsync(int id);
    Task<List<Mesa>> GetAllAsync();
    Task AddAsync(Mesa mesa);
    void Update(Mesa mesa);
    void Delete(Mesa mesa);
}