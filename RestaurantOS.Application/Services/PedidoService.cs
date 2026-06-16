using RestaurantOS.Application.DTOs.Pedido;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Domain.Entities;
using RestaurantOS.Domain.Enums;
using RestaurantOS.Domain.Exceptions;
using RestaurantOS.Domain.Interfaces;

namespace RestaurantOS.Application.Services;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IMesaRepository _mesaRepository;
    private readonly IProductoRepository _productoRepository;

    public PedidoService(
        IPedidoRepository pedidoRepository,
        IMesaRepository mesaRepository,
        IProductoRepository productoRepository)
    {
        _pedidoRepository = pedidoRepository;
        _mesaRepository = mesaRepository;
        _productoRepository = productoRepository;
    }

    public async Task<PedidoDto?> GetByIdAsync(int id)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(id);
        if (pedido is null) return null;

        return MapToDto(pedido);
    }

    public async Task<List<PedidoDto>> GetAllAsync()
    {
        var pedidos = await _pedidoRepository.GetAllAsync();
        return pedidos.Select(MapToDto).ToList();
    }

    public async Task<List<PedidoDto>> GetByEstadoAsync(EstadoPedido estado)
    {
        var pedidos = await _pedidoRepository.GetByEstadoAsync(estado);
        return pedidos.Select(MapToDto).ToList();
    }

    public async Task<PedidoDto> CrearAsync(CrearPedidoDto dto)
    {
        // 1. Validar que la mesa existe y está disponible
        var mesa = await _mesaRepository.GetByIdAsync(dto.MesaId);
        if (mesa is null)
            throw new KeyNotFoundException($"Mesa {dto.MesaId} no encontrada.");

        if (mesa.Estado == EstadoMesa.Ocupada)
            throw new MesaOcupadaException(mesa.Id);

        // 2. Crear el pedido base
        var pedido = new Pedido
        {
            MesaId = mesa.Id,
            Mesa = mesa,
            Estado = EstadoPedido.Pendiente,
            FechaCreacion = DateTime.UtcNow
        };

        // 3. Recorrer cada producto solicitado, validar y armar el detalle
        foreach (var item in dto.Productos)
        {
            var producto = await _productoRepository.GetByIdAsync(item.ProductoId);
            if (producto is null)
                throw new KeyNotFoundException($"Producto {item.ProductoId} no encontrado.");

            if (!producto.Disponible)
                throw new ProductoNoDisponibleException(producto.Nombre);

            pedido.Detalles.Add(new DetallePedido
            {
                ProductoId = producto.Id,
                Producto = producto,
                Cantidad = item.Cantidad,
                PrecioUnitario = producto.Precio, // precio real, no el que mande el cliente
                Notas = item.Notas
            });
        }

        // 4. Guardar el pedido y marcar la mesa como ocupada
        await _pedidoRepository.AddAsync(pedido);

        mesa.Estado = EstadoMesa.Ocupada;
        _mesaRepository.Update(mesa);

        return MapToDto(pedido);
    }

    public async Task CambiarEstadoAsync(int id, EstadoPedido nuevoEstado)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(id);
        if (pedido is null)
            throw new KeyNotFoundException($"Pedido {id} no encontrado.");

        if (pedido.Estado == EstadoPedido.Cancelado)
            throw new PedidoYaCanceladoException(pedido.Id);

        pedido.Estado = nuevoEstado;
        _pedidoRepository.Update(pedido);
    }

    private static PedidoDto MapToDto(Pedido pedido)
    {
        return new PedidoDto
        {
            Id = pedido.Id,
            MesaId = pedido.MesaId,
            NumeroMesa = pedido.Mesa?.Numero ?? 0,
            FechaCreacion = pedido.FechaCreacion,
            Estado = pedido.Estado,
            Detalles = pedido.Detalles.Select(d => new DetallePedidoDto
            {
                ProductoId = d.ProductoId,
                NombreProducto = d.Producto?.Nombre ?? string.Empty,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                Notas = d.Notas
            }).ToList(),
            Total = pedido.Detalles.Sum(d => d.Cantidad * d.PrecioUnitario)
        };
    }
}