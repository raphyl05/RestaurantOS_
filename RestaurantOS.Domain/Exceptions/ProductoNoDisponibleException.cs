namespace RestaurantOS.Domain.Exceptions;

public class ProductoNoDisponibleException : DomainException
{
    public ProductoNoDisponibleException(string nombreProducto)
        : base($"El producto '{nombreProducto}' no está disponible actualmente.")
    {
    }
}