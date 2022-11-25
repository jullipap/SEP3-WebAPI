using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
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

    public Task<string> JoinRide(Domain.DTOs.JoinRideDto dto)
    {
        return rideDao.JoinRide(dto.RideId, dto.Name, dto.Phone);
    }

    public Task<Ride> createRide(RideCreationDto dto)
    {
        //TODO @bartoszwiacek
        throw new NotImplementedException();
    }
}