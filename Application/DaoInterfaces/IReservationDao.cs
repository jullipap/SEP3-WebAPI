using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IReservationDao
{
     Task<Reservation> AcceptPassenger(int reservationId, bool didAccept);
    Task<ICollection<Reservation>> GetReservationToAccept();
}