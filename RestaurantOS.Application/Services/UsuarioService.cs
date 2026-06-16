using RestaurantOS.Application.DTOs.Usuario;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Domain.Entities;
using RestaurantOS.Domain.Interfaces;

namespace RestaurantOS.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UsuarioService(IUsuarioRepository usuarioRepository, IPasswordHasher passwordHasher)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UsuarioDto?> GetByIdAsync(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario is null) return null;

        return MapToDto(usuario);
    }

    public async Task<List<UsuarioDto>> GetAllAsync()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        return usuarios.Select(MapToDto).ToList();
    }

    public async Task<UsuarioDto> CrearAsync(CrearUsuarioDto dto)
    {
        var usuario = new Usuario
        {
            Nombre = dto.Nombre,
            Email = dto.Email,
            PasswordHash = _passwordHasher.Hash(dto.Password),
            Rol = dto.Rol,
            Activo = true
        };

        await _usuarioRepository.AddAsync(usuario);

        return MapToDto(usuario);
    }

    public async Task<UsuarioDto?> LoginAsync(LoginDto dto)
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        var usuario = usuarios.FirstOrDefault(u => u.Email == dto.Email && u.Activo);

        if (usuario is null) return null;

        var passwordValida = _passwordHasher.Verify(dto.Password, usuario.PasswordHash);
        if (!passwordValida) return null;

        return MapToDto(usuario);
    }

    private static UsuarioDto MapToDto(Usuario usuario)
    {
        return new UsuarioDto
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Email = usuario.Email,
            Rol = usuario.Rol,
            Activo = usuario.Activo
        };
    }
}