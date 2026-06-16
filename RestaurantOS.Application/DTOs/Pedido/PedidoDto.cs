using RestaurantOS.Domain.Enums;

namespace RestaurantOS.Application.DTOs.Pedido;

public class PedidoDto
{
    public int Id { get; set; }
    public int MesaId { get; set; }
    public int NumeroMesa { get; set; }
    public DateTime FechaCreacion { get; set; }
    public EstadoPedido Estado { get; set; }
    public List<DetallePedidoDto> Detalles { get; set; } = new();
    public decimal Total { get; set; }
}