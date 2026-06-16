using RestaurantOS.Application.DTOs.Usuario;

namespace RestaurantOS.Application.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioDto?> GetByIdAsync(int id);
    Task<List<UsuarioDto>> GetAllAsync();
    Task<UsuarioDto> CrearAsync(CrearUsuarioDto dto);
    Task<UsuarioDto?> LoginAsync(LoginDto dto);
}