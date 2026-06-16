namespace RestaurantOS.Application.DTOs.Pedido;

public class CrearPedidoDto
{
    public int MesaId { get; set; }
    public List<CrearDetallePedidoDto> Productos { get; set; } = new();
}