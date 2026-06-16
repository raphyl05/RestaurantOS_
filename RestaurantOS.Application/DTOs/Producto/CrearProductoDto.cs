using RestaurantOS.Domain.Enums;

namespace RestaurantOS.Application.DTOs.Producto;

public class CrearProductoDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public CategoriaProducto Categoria { get; set; }
}