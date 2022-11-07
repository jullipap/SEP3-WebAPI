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
        using var channel = GrpcChannel.ForAddress("https://localhost:5443");
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
               country = rideMessage.StartLocation.Country, city = rideMessage.StartLocation.City, coordinatesX = rideMessage.StartLocation.CoordinateX,
               coordinatesY = rideMessage.StartLocation.CoordinateY, streetName = rideMessage.StartLocation.Street, zipCode = rideMessage.StartLocation.Zipcode
           };
           
           Location endLocation = new Location()
           {
               country = rideMessage.EndLocation.Country, city = rideMessage.EndLocation.City, coordinatesX = rideMessage.EndLocation.CoordinateX,
               coordinatesY = rideMessage.EndLocation.CoordinateY, streetName = rideMessage.EndLocation.Street, zipCode = rideMessage.EndLocation.Zipcode
           };

           DateTime dateTime = new DateTime() {epoch = rideMessage.StartTime};
           
           rides.Add(new Ride(){Id = rideMessage.Id, Driver = rideMessage.Driver.Name, dateTime = dateTime, destination = endLocation, startLocation = startLocation});
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