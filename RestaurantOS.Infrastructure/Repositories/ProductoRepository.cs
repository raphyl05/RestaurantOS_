using Microsoft.EntityFrameworkCore;
using RestaurantOS.Domain.Entities;
using RestaurantOS.Domain.Interfaces;
using RestaurantOS.Infrastructure.Data;

namespace RestaurantOS.Infrastructure.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly ApplicationDbContext _context;

    public ProductoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Producto?> GetByIdAsync(int id)
    {
        return await _context.Productos.FindAsync(id);
    }

    public async Task<List<Producto>> GetAllAsync()
    {
        return await _context.Productos.ToListAsync();
    }

    public async Task AddAsync(Producto producto)
    {
        await _context.Productos.AddAsync(producto);
        await _context.SaveChangesAsync();
    }

    public void Update(Producto producto)
    {
        _context.Productos.Update(producto);
        _context.SaveChanges();
    }

    public void Delete(Producto producto)
    {
        _context.Productos.Remove(producto);
        _context.SaveChanges();
    }
}