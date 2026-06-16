using Microsoft.EntityFrameworkCore;
using RestaurantOS.Domain.Entities;
using RestaurantOS.Domain.Interfaces;
using RestaurantOS.Infrastructure.Data;

namespace RestaurantOS.Infrastructure.Repositories;

public class PagoRepository : IPagoRepository
{
    private readonly ApplicationDbContext _context;

    public PagoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Pago?> GetByIdAsync(int id)
    {
        return await _context.Pagos.FindAsync(id);
    }

    public async Task<List<Pago>> GetAllAsync()
    {
        return await _context.Pagos.ToListAsync();
    }

    public async Task AddAsync(Pago pago)
    {
        await _context.Pagos.AddAsync(pago);
        await _context.SaveChangesAsync();
    }

    public void Update(Pago pago)
    {
        _context.Pagos.Update(pago);
        _context.SaveChanges();
    }

    public void Delete(Pago pago)
    {
        _context.Pagos.Remove(pago);
        _context.SaveChanges();
    }
}