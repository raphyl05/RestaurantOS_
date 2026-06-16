using Microsoft.EntityFrameworkCore;
using RestaurantOS.Domain.Entities;
using RestaurantOS.Domain.Interfaces;
using RestaurantOS.Infrastructure.Data;

namespace RestaurantOS.Infrastructure.Repositories;

public class MesaRepository : IMesaRepository
{
    private readonly ApplicationDbContext _context;

    public MesaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Mesa?> GetByIdAsync(int id)
    {
        return await _context.Mesas.FindAsync(id);
    }

    public async Task<List<Mesa>> GetAllAsync()
    {
        return await _context.Mesas.ToListAsync();
    }

    public async Task AddAsync(Mesa mesa)
    {
        await _context.Mesas.AddAsync(mesa);
        await _context.SaveChangesAsync();
    }

    public void Update(Mesa mesa)
    {
        _context.Mesas.Update(mesa);
        _context.SaveChanges();
    }

    public void Delete(Mesa mesa)
    {
        _context.Mesas.Remove(mesa);
        _context.SaveChanges();
    }
}