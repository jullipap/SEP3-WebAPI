using Application.DAOs;
using Domain.Models;
using DateTime = Domain.Models.DateTime;

namespace Application.DaoInterfaces;

public interface IRideDao
{
    Task<List<Ride>> GetAllAsync(DateTime? startDate, DateTime? endDate, long epochNow, int userId);
    Task<string> JoinRide(int userId, int rideId);

    Task<Ride> CreateRide(Location startLocation, Location destination, DateTime startDate, int driverId,
        int capacity);
    
    Task<List<Ride>> GetRidesByDriverId(int driverId);
}