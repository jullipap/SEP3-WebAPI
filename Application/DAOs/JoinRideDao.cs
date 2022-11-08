using Application.DaoInterfaces;
using Domain.Models;
using Grpc.Net.Client;
using DateTime = Domain.Models.DateTime;

namespace Application.DAOs;

public class JoinRideDao : IRideDao
{

    private Rides.RidesClient client;
    
    public JoinRideDao()
    {
         var channel = GrpcChannel.ForAddress("http://localhost:5434");
        client = new Rides.RidesClient(channel);
    }

    public async Task<List<Ride>> GetAllAsync()
   {
       var reply = await client.getRidesAsync(new EpochTimelineMessage {});
       List<Ride> rides = new List<Ride>();
       foreach (var rideMessage in reply.Rides)
       {
           Location startLocation = new Location()
           {
               Country = rideMessage.StartLocation.Country, City = rideMessage.StartLocation.City, CoordinatesX = rideMessage.StartLocation.CoordinateX,
               CoordinatesY = rideMessage.StartLocation.CoordinateY, StreetName = rideMessage.StartLocation.Street, ZipCode = rideMessage.StartLocation.Zipcode
           };
           
           Location endLocation = new Location()
           {
               Country = rideMessage.EndLocation.Country, City = rideMessage.EndLocation.City, CoordinatesX = rideMessage.EndLocation.CoordinateX,
               CoordinatesY = rideMessage.EndLocation.CoordinateY, StreetName = rideMessage.EndLocation.Street, ZipCode = rideMessage.EndLocation.Zipcode
           };
   
           DateTime dateTime = new DateTime(rideMessage.StartTime) ;
           
           rides.Add(new Ride(){Id = rideMessage.Id, DriverName = rideMessage.Driver.Name, Date = dateTime, Destination = endLocation, StartLocation = startLocation});
       }
   
       return rides;
   }

    public async Task<string> JoinRide(int Id, string name, string phone)
    {
        JoinRideDto joinRideDto = new JoinRideDto
        {
            RideId = Id,
            PassengerName = name,
            PassengerPhone = phone
        };
    
        var reply = await client.joinRideAsync(joinRideDto);
        return reply.ConfirmationMessage_;
    }

   //  public async Task<string> JoinRide(JoinRideDto dto)
   // {
   //     JoinRideDto joinRideDto = new JoinRideDto
   //     {
   //         RideId = dto.RideId,
   //         PassengerName = dto.PassengerName,
   //         PassengerPhone = dto.PassengerPhone
   //     };
   //
   //     var reply = await client.joinRideAsync(joinRideDto);
   //     return reply.ConfirmationMessage_;
   // }
   
   //sorry i messed up sth with interfaces 
   //(I had DTO objects in IRideDao interface) (Yes, I've done it again)
   //I think now it should be correct
   //but just in case I'm gonna leave it here
}