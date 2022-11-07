using Domain.Models;

namespace Application.LogicInterfaces;

public interface IRideLogic
{
    Task<List<Ride>> GetAllAsync();
    Task<string> JoinRide(JoinRideDto dto);
}