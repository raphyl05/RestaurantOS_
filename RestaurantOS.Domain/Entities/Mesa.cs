using RestaurantOS.Domain.Enums;

namespace RestaurantOS.Domain.Entities

{
    public class Mesa
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int Capacidad { get; set; }
        public EstadoMesa Estado {  get; set; }
    }
}
