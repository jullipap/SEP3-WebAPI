using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class ReservationLogic : IReservationLogic
{
    private readonly IReservationDao reservationDao;

    public ReservationLogic(IReservationDao reservationDao)
    {
        this.reservationDao = reservationDao;
    }

    public Task<Reservation> AcceptPassenger(AcceptReservationDto acceptDto)
    {
        return reservationDao.AcceptPassenger(acceptDto.ReservationId, acceptDto.DidAccept);
    }

    public Task<ICollection<Reservation>> GetReservationToAccept(int id)
    {
        return reservationDao.GetReservationToAccept(id);
    }

    public Task<List<Reservation>> GetAcceptedReservationsByRideId(int rideId)
    {
        return reservationDao.GetAcceptedReservationsByRideId(rideId);
    }
}