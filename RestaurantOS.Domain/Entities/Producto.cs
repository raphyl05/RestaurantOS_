using RestaurantOS.Domain.Enums;

namespace RestaurantOS.Domain.Entities
{
    public class Producto
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string descripcion {  get; set; }
        public decimal Precio { get; set; }
        public bool Disponible { get; set; }
        public CategoriaProducto Categoria {  get; set; }

    }
}
