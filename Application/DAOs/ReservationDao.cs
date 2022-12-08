﻿using Application.DaoInterfaces;
using Domain.Models;
using Grpc.Net.Client;

namespace Application.DAOs;

public class ReservationDao : IReservationDao
{
    //Ive done this without importing new protofile

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
            isAccepted = reply.DidAccept.Value,
            passengerName = reply.Name,
            reservationId = reply.Id,
            rideId = reply.RideId
        };
        return reservation;
    }

    public async Task<ICollection<Reservation>> GetReservationToAccept(int id)
    {
        var reply = await client.getAllReservationsToAcceptAsync(new IdMessage(){DriverId = id});
        List<Reservation> reservations = new List<Reservation>();

        foreach (var reservation in reply.ReservationMessages)
        {
            reservations.Add(new Reservation()
            {
              passengerName  = reservation.Name,
              isAccepted = null,
              reservationId = reservation.Id,
              rideId = reservation.RideId
            });
        }
        return reservations;
    }

    public async Task<List<Reservation>> getAcceptedReservationsByRideId(int rideId)
    {
        var reply = client.getAcceptedReservationsByRideId(new RideIdMessage() {RideId = rideId}); //maybe await
        List<Reservation> reservations = new List<Reservation>();

        foreach (var reservation in reply.ReservationMessages)
        {
            reservations.Add(new Reservation()
            {
                passengerName  = reservation.Name,
                isAccepted = true,
                reservationId = reservation.Id,
                rideId = rideId
            });
        }
        return reservations;
    }

}