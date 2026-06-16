using RestaurantOS.Domain.Enums;

namespace RestaurantOS.Application.DTOs.Pago;

public class CrearPagoDto
{
    public int PedidoId { get; set; }
    public MetodoPago Metodo { get; set; }
}