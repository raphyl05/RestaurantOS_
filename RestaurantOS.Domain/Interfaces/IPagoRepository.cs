using RestaurantOS.Domain.Entities;

namespace RestaurantOS.Domain.Interfaces;

public interface IPagoRepository
{
    Task<Pago?> GetByIdAsync(int id);
    Task<List<Pago>> GetAllAsync();
    Task AddAsync(Pago pago);
    void Update(Pago pago);
    void Delete(Pago pago);
}