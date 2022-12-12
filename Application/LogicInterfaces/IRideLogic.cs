using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IRideLogic
{
    Task<List<Ride>> GetAllAsync(string? startDate, string? endDate, string userId);
    Task<string> JoinRide(JoinRideDto dto);

    Task<Ride> CreateRide(RideCreationDto dto);

    Task<List<Ride>> GetRidesByDriverId(int driverId);
    Task<Ride> GetRideByIdAsync(int id);

    Task ChangeRideStatusAsync(ChangeStatusDto dto);
}