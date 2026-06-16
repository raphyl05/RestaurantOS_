using RestaurantOS.Domain.Enums;

namespace RestaurantOS.Domain.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public int MesaId { get; set; }
        public Mesa Mesa { get; set; } = null!;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public EstadoPedido Estado { get; set; } = EstadoPedido.Pendiente;
        public List<DetallePedido> Detalles { get; set; }
    }
}
