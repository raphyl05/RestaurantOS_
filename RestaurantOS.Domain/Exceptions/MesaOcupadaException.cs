namespace RestaurantOS.Domain.Exceptions;

public class MesaOcupadaException : DomainException
{
    public MesaOcupadaException(int mesaId)
        : base($"La mesa {mesaId} ya está ocupada y no puede recibir un nuevo pedido.")
    {
    }
}