namespace RestaurantOS.Domain.Exceptions;

public class PedidoYaCanceladoException : DomainException
{
    public PedidoYaCanceladoException(int pedidoId)
        : base($"El pedido {pedidoId} ya fue cancelado y no puede modificarse.")
    {
    }
}