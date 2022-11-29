using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using DateTime = Domain.Models.DateTime;

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

    public Task<Ride> CreateRide(RideCreationDto createRideDto)
    {
        // date /
        // time :
        // sekund dac na 0

        string[] timeArr = createRideDto.Time.Split(":");
        string[] dateArr = createRideDto.Date.Split("/");
        int minutes = Int32.Parse(timeArr[1]);
        int hour = Int32.Parse(timeArr[0]);

        int year = Int32.Parse(dateArr[2]);
        int month = Int32.Parse(dateArr[1]);
        int day = Int32.Parse(dateArr[0]);

        DateTime date = new DateTime(month, day, year, hour, minutes, 0);
        return rideDao.CreateRide(createRideDto.StartLocation, createRideDto.Destination, date,
            createRideDto.DriversName, createRideDto.Capacity);
    }
}