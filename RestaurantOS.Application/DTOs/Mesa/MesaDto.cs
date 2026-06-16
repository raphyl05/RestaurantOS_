using RestaurantOS.Domain.Enums;

namespace RestaurantOS.Application.DTOs.Mesa;

public class MesaDto
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public int Capacidad { get; set; }
    public EstadoMesa Estado { get; set; }
}