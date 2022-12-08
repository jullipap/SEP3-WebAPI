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
    
    public Task<List<Ride>> GetAllAsync(string? startDate, string? endDate)
    {
        DateTime? startDateTime = null;
        if (startDate != null)
        {
            string[] startDateArr = startDate.Split("/");
        
            int startYear = Int32.Parse(startDateArr[2]);
            int startMonth = Int32.Parse(startDateArr[1]);
            int startDay = Int32.Parse(startDateArr[0]);

             startDateTime = new DateTime(startMonth, startDay, startYear,0,0,0);

        }

        DateTime? endDateTime = null;

        if (endDate != null)
        {
            string[] endDateArr = endDate.Split("/");
        
            int endYear = Int32.Parse(endDateArr[2]);
            int endMonth = Int32.Parse(endDateArr[1]);
            int endDay = Int32.Parse(endDateArr[0]);

             endDateTime  = new DateTime(endMonth, endDay, endYear,23,59,59);
        }
        
        
        return rideDao.GetAllAsync(startDateTime, endDateTime);
        
    }

    public Task<string> JoinRide(JoinRideDto dto)
    {
        return rideDao.JoinRide(dto.UserId, dto.RideId);
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
            createRideDto.DriverId, createRideDto.Capacity);
    }
}