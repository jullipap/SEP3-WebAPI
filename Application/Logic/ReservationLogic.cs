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

    public Task<ICollection<Reservation>> GetAllReservationsByUserIdAsync(int id)
    {
        return reservationDao.GetAllReservationsByUserIdAsync(id);
    }

    public Task ChangeReservationStatusAsync(ChangeStatusDto dto)
    {
        if (dto.Status.Equals("Pending") || dto.Status.Equals("Accepted") || dto.Status.Equals("Rejected")
            || dto.Status.Equals("Cancelled by driver") || dto.Status.Equals("Cancelled by passenger"))
        {
            return reservationDao.ChangeReservationStatusAsync(dto.Id, dto.Status);
        }
        throw new Exception("Invalid status");
        
    }

    public Task<List<Reservation>> GetAcceptedReservationsByRideId(int rideId)
    {
        return reservationDao.GetAcceptedReservationsByRideId(rideId);
    }
}