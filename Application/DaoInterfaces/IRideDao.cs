using Application.DAOs;
using Domain.Models;
using DateTime = Domain.Models.DateTime;

namespace Application.DaoInterfaces;

public interface IRideDao
{
    Task<List<Ride>> GetAllAsync();
    Task<string> JoinRide(int id, string name, string phone);

    Task<Ride> CreateRide(Location startLocation, Location destination, DateTime startDate, string driver,
        int capacity);
}