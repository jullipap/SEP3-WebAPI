using Application.DaoInterfaces;
using Domain.Models;
using Grpc.Net.Client;

namespace Application.DAOs;

public class ReservationDao : IReservationDao
{

    private Reservations.ReservationsClient client;

    public ReservationDao()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5434");
        client = new Reservations.ReservationsClient(channel);
    }

    public async Task<Reservation> AcceptPassenger(int reservationId, bool didAccept)
    {
        AcceptMessage acceptMessage = new AcceptMessage()
        {
            ReservationId = reservationId,
            DidAccept = didAccept
        };
        var reply = await client.acceptPassengerAsync(acceptMessage);
        Reservation reservation = new Reservation()
        {
            //should those variables start with big letters?
            IsAccepted = reply.DidAccept.Value,
            PassengerName = reply.Name,
            ReservationId = reply.Id,
            RideId = reply.RideId,
            Status = reply.Status

        };
        return reservation;
    }

    public async Task<ICollection<Reservation>> GetReservationToAccept(int id)
    {
        var reply = await client.getAllReservationsToAcceptAsync(new IdMessage(){Id = id});
        List<Reservation> reservations = new List<Reservation>();

        foreach (var reservation in reply.ReservationMessages)
        {
            reservations.Add(new Reservation()
            {
              PassengerName  = reservation.Name,
              IsAccepted = null,
              ReservationId = reservation.Id,
              RideId = reservation.RideId,
              Status = reservation.Status
            });
        }
        return reservations;
    }

    public async Task<ICollection<Reservation>> GetAllReservationsByUserIdAsync(int id)
    {
        var reply = await client.getAllReservationsByUserIdAsync(new IdMessage() {Id = id}); 
        List<Reservation> reservations = new List<Reservation>();

        foreach (var reservation in reply.ReservationMessages)
        {
            reservations.Add(new Reservation()
            {
                PassengerName  = reservation.Name,
                IsAccepted = true,
                ReservationId = reservation.Id,
                RideId = reservation.RideId,
                Status = reservation.Status
            });
        }
        return reservations;    }

    public async Task ChangeReservationStatusAsync(int id, string status)
    {
        var reply = await client.changeReservationStatusAsync(new ChangeReservStatusMessage() {Id = id, Status = status});

        if (!reply.Value)
        {
            throw new Exception("Status was not successfully changed");
        }
    }

    public async Task<List<Reservation>> GetAcceptedReservationsByRideId(int rideId)
    {
        var reply = await client.getAcceptedReservationsByRideIdAsync(new IdMessage() {Id = rideId}); 
        List<Reservation> reservations = new List<Reservation>();

        foreach (var reservation in reply.ReservationMessages)
        {
            reservations.Add(new Reservation()
            {
                PassengerName  = reservation.Name,
                IsAccepted = true,
                ReservationId = reservation.Id,
                RideId = rideId,
                Status = reservation.Status
            });
        }
        return reservations;
    }

}