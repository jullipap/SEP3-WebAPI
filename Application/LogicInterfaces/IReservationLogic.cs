using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IReservationLogic
{
    Task<Reservation> AcceptPassenger(AcceptReservationDto acceptDto);


    Task<List<Reservation>> GetAcceptedReservationsByRideId(int rideId);

    Task<ICollection<Reservation>> GetReservationToAccept(int id);
}