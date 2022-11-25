using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IRideLogic
{
    Task<List<Ride>> GetAllAsync();
    Task<string> JoinRide(Domain.DTOs.JoinRideDto dto);

    Task<Ride> createRide(RideCreationDto dto);
}