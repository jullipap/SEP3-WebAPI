using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IRideLogic
{
    Task<List<Ride>> GetAllAsync(string? startDate, string? endDate);
    Task<string> JoinRide(Domain.DTOs.JoinRideDto dto);

    Task<Ride> CreateRide(RideCreationDto dto);

    Task<List<Ride>> GetRidesByDriverId(int driverId);
}