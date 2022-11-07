using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.Models;

namespace Application.Logic;

public class RideLogic : IRideLogic
{
    private readonly IRideDao rideDao;

    public RideLogic(IRideDao rideDao)
    {
        this.rideDao = rideDao;
    }
    
    public Task<List<Ride>> GetAllAsync()
    {
        return rideDao.GetAllAsync();
    }

    public Task<string> JoinRide(JoinRideDto dto)
    {
        return rideDao.JoinRide(dto.RideId, dto.PassengerName, dto.PassengerPhone);
    }
}