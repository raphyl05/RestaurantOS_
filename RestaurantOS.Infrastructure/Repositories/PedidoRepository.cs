using Microsoft.EntityFrameworkCore;
using RestaurantOS.Domain.Entities;
using RestaurantOS.Domain.Enums;
using RestaurantOS.Domain.Interfaces;
using RestaurantOS.Infrastructure.Data;

namespace RestaurantOS.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly ApplicationDbContext _context;

    public PedidoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Pedido?> GetByIdAsync(int id)
    {
        return await _context.Pedidos
            .Include(p => p.Mesa)
            .Include(p => p.Detalles)
                .ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Pedido>> GetAllAsync()
    {
        return await _context.Pedidos
            .Include(p => p.Mesa)
            .Include(p => p.Detalles)
                .ThenInclude(d => d.Producto)
            .ToListAsync();
    }

    public async Task<List<Pedido>> GetByEstadoAsync(EstadoPedido estado)
    {
        return await _context.Pedidos
            .Include(p => p.Mesa)
            .Include(p => p.Detalles)
                .ThenInclude(d => d.Producto)
            .Where(p => p.Estado == estado)
            .ToListAsync();
    }

    public async Task<List<Pedido>> GetByMesaIdAsync(int mesaId)
    {
        return await _context.Pedidos
            .Include(p => p.Mesa)
            .Include(p => p.Detalles)
                .ThenInclude(d => d.Producto)
            .Where(p => p.MesaId == mesaId)
            .ToListAsync();
    }

    public async Task AddAsync(Pedido pedido)
    {
        await _context.Pedidos.AddAsync(pedido);
        await _context.SaveChangesAsync();
    }

    public void Update(Pedido pedido)
    {
        _context.Pedidos.Update(pedido);
        _context.SaveChanges();
    }

    public void Delete(Pedido pedido)
    {
        _context.Pedidos.Remove(pedido);
        _context.SaveChanges();
    }
}