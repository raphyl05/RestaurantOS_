using RestaurantOS.Domain.Enums;

namespace RestaurantOS.Domain.Entities;

public class Pago
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;
    public decimal Monto { get; set; }
    public MetodoPago Metodo { get; set; }
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
}