using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IReservationLogic
{
    Task<Reservation> AcceptPassenger(AcceptReservationDto acceptDto);
    Task<ICollection<Reservation>> GetReservationToAccept(int id);
}