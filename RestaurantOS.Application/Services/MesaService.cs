using RestaurantOS.Application.DTOs.Mesa;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Domain.Entities;
using RestaurantOS.Domain.Enums;
using RestaurantOS.Domain.Interfaces;

namespace RestaurantOS.Application.Services;

public class MesaService : IMesaService
{
    private readonly IMesaRepository _mesaRepository;

    public MesaService(IMesaRepository mesaRepository)
    {
        _mesaRepository = mesaRepository;
    }

    public async Task<MesaDto?> GetByIdAsync(int id)
    {
        var mesa = await _mesaRepository.GetByIdAsync(id);
        if (mesa is null) return null;

        return MapToDto(mesa);
    }

    public async Task<List<MesaDto>> GetAllAsync()
    {
        var mesas = await _mesaRepository.GetAllAsync();
        return mesas.Select(MapToDto).ToList();
    }

    public async Task<MesaDto> CrearAsync(CrearMesaDto dto)
    {
        var mesa = new Mesa
        {
            Numero = dto.Numero,
            Capacidad = dto.Capacidad,
            Estado = EstadoMesa.Disponible
        };

        await _mesaRepository.AddAsync(mesa);

        return MapToDto(mesa);
    }

    public async Task CambiarEstadoAsync(int id, EstadoMesa nuevoEstado)
    {
        var mesa = await _mesaRepository.GetByIdAsync(id);
        if (mesa is null)
            throw new KeyNotFoundException($"Mesa {id} no encontrada.");

        mesa.Estado = nuevoEstado;
        _mesaRepository.Update(mesa);
    }

    public async Task DeleteAsync(int id)
    {
        var mesa = await _mesaRepository.GetByIdAsync(id);
        if (mesa is null)
            throw new KeyNotFoundException($"Mesa {id} no encontrada.");

        _mesaRepository.Delete(mesa);
    }

    private static MesaDto MapToDto(Mesa mesa)
    {
        return new MesaDto
        {
            Id = mesa.Id,
            Numero = mesa.Numero,
            Capacidad = mesa.Capacidad,
            Estado = mesa.Estado
        };
    }
}