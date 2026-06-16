namespace RestaurantOS.Application.DTOs.Pedido;

public class DetallePedidoDto
{
    public int ProductoId { get; set; }
    public string NombreProducto { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal => Cantidad * PrecioUnitario;
    public string? Notas { get; set; }
}