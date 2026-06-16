namespace RestaurantOS.Application.DTOs.Pedido;

public class CrearDetallePedidoDto
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public string? Notas { get; set; }
}