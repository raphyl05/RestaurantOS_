using RestaurantOS.Domain.Enums;

namespace RestaurantOS.Application.DTOs.Usuario;

public class CrearUsuarioDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; // texto plano, solo de entrada
    public RolUsuario Rol { get; set; }
}