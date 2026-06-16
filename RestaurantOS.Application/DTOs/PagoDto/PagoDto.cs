using RestaurantOS.Domain.Enums;

namespace RestaurantOS.Application.DTOs.PagoDto;

public class PagoDto
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public decimal Monto { get; set; }
    public MetodoPago Metodo { get; set; }
    public DateTime Fecha { get; set; }
}