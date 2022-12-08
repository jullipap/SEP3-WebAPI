using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Grpc.Net.Client;
using DateTime = Domain.Models.DateTime;

namespace Application.DAOs;

public class RideDao : IRideDao
//Class name needs to be changed 
{

    private Rides.RidesClient client;

    public RideDao()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5434");
        client = new Rides.RidesClient(channel);
    }

    public async Task<List<Ride>> GetAllAsync(DateTime? startDate, DateTime? endDate)
    {
        EpochTimelineMessage epochMessage = new EpochTimelineMessage();
        if (startDate != null && endDate != null)
        {
            epochMessage.EpochLowerBound = startDate.Epoch;
            epochMessage.EpochUpperBound = endDate.Epoch;
        }
       var reply = await client.getRidesAsync(epochMessage);
       List<Ride> rides = new List<Ride>();
       foreach (var rideMessage in reply.Rides)
       {
           Location startLocation = new Location()
           {
               Country = rideMessage.StartLocation.Country, 
               City = rideMessage.StartLocation.City, 
               CoordinatesX = rideMessage.StartLocation.CoordinateX,
               CoordinatesY = rideMessage.StartLocation.CoordinateY, 
               StreetName = rideMessage.StartLocation.Street, 
               ZipCode = rideMessage.StartLocation.Zipcode
           };
           
           Location endLocation = new Location()
           {
               Country = rideMessage.Destination.Country,
               City = rideMessage.Destination.City,
               CoordinatesX = rideMessage.Destination.CoordinateX,
               CoordinatesY = rideMessage.Destination.CoordinateY, 
               StreetName = rideMessage.Destination.Street, 
               ZipCode = rideMessage.Destination.Zipcode
           };

           DateTime dateTime = new DateTime(rideMessage.StartDate) ;
           
           rides.Add(new Ride(endLocation,dateTime,startLocation,rideMessage.Id,rideMessage.Driver.Id, rideMessage.Capacity));
       }
   
       return rides;
   }

    public async Task<string> JoinRide(int userId, int rideId)
    {
        JoinRideMessage joinRideDto = new JoinRideMessage()
        {
            RideId = rideId,
            UserId = userId //need to fix proto - done
        };
    
        var reply = await client.joinRideAsync(joinRideDto);
        return reply.ConfirmationMessage_;
    }
    
    public async Task<Ride> CreateRide(Location startLocation, Location destination, DateTime startDate, int driverId, int capacity)
    {
        LocationMessage destinationTemp = new LocationMessage()
        {
            Country = destination.Country,
            City = destination.City,
            CoordinateX = destination.CoordinatesX,
            CoordinateY = destination.CoordinatesY,
            Street = destination.StreetName,
            Zipcode = destination.ZipCode
        };
        
        LocationMessage startLocationTemp = new LocationMessage()
        {
            Country = startLocation.Country,
            City = startLocation.City,
            CoordinateX = startLocation.CoordinatesX,
            CoordinateY = startLocation.CoordinatesY,
            Street = startLocation.StreetName,
            Zipcode = startLocation.ZipCode
        };

        CreateRideMessage createRideMessage = new CreateRideMessage()
        {
            Capacity = capacity,
            Destination = destinationTemp,
            DriverId = driverId,
            StartDate = startDate.Epoch,
            StartLocation = startLocationTemp
        };
        //start date epoch
        var reply = await client.createRideAsync(createRideMessage);
        Location destinationFinal = new Location()
        {
            Country = reply.Destination.Country,
            City = reply.Destination.City,
            CoordinatesX = reply.Destination.CoordinateX,
            CoordinatesY = reply.Destination.CoordinateY,
            StreetName = reply.Destination.Street,
            ZipCode = reply.Destination.Zipcode
        };
        Location startLocationFinal = new Location()
        {
            Country = reply.StartLocation.Country,
            City = reply.StartLocation.City,
            CoordinatesX = reply.StartLocation.CoordinateX,
            CoordinatesY = reply.StartLocation.CoordinateY,
            StreetName = reply.StartLocation.Street,
            ZipCode = reply.StartLocation.Zipcode
        };
        Ride ride = new Ride()
        {
            Destination = destinationFinal,
            Capacity = capacity,
            DriverId = driverId,
            Id = reply.Id,
            StartDate = new DateTime(reply.StartDate),
            StartLocation = startLocationFinal
        };
        return ride;
    }
}