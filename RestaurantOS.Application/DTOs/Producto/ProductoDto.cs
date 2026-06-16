using RestaurantOS.Domain.Enums;

namespace RestaurantOS.Application.DTOs.Producto
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public bool Disponible { get; set; }
        public CategoriaProducto Categoria {  get; set; }
    }
}
