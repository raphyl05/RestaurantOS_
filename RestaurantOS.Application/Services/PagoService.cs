using RestaurantOS.Application.DTOs.Pago;
using RestaurantOS.Application.DTOs.PagoDto;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Domain.Entities;
using RestaurantOS.Domain.Enums;
using RestaurantOS.Domain.Exceptions;
using RestaurantOS.Domain.Interfaces;

namespace RestaurantOS.Application.Services;

public class PagoService : IPagoService
{
    private readonly IPagoRepository _pagoRepository;
    private readonly IPedidoRepository _pedidoRepository;

    public PagoService(IPagoRepository pagoRepository, IPedidoRepository pedidoRepository)
    {
        _pagoRepository = pagoRepository;
        _pedidoRepository = pedidoRepository;
    }

    public async Task<PagoDto?> GetByIdAsync(int id)
    {
        var pago = await _pagoRepository.GetByIdAsync(id);
        if (pago is null) return null;

        return MapToDto(pago);
    }

    public async Task<List<PagoDto>> GetAllAsync()
    {
        var pagos = await _pagoRepository.GetAllAsync();
        return pagos.Select(MapToDto).ToList();
    }

    public async Task<PagoDto> CrearAsync(CrearPagoDto dto)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(dto.PedidoId);
        if (pedido is null)
            throw new KeyNotFoundException($"Pedido {dto.PedidoId} no encontrado.");

        if (pedido.Estado == EstadoPedido.Cancelado)
            throw new PedidoYaCanceladoException(pedido.Id);

        var montoReal = pedido.Detalles.Sum(d => d.Cantidad * d.PrecioUnitario);

        var pago = new Pago
        {
            PedidoId = pedido.Id,
            Pedido = pedido,
            Monto = montoReal,
            Metodo = dto.Metodo,
            Fecha = DateTime.UtcNow
        };

        await _pagoRepository.AddAsync(pago);

        pedido.Estado = EstadoPedido.Entregado;
        _pedidoRepository.Update(pedido);

        return MapToDto(pago);
    }

    private static PagoDto MapToDto(Pago pago)
    {
        return new PagoDto
        {
            Id = pago.Id,
            PedidoId = pago.PedidoId,
            Monto = pago.Monto,
            Metodo = pago.Metodo,
            Fecha = pago.Fecha
        };
    }
}