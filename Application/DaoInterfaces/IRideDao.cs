using Application.DAOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IRideDao
{
    Task<List<Ride>> GetAllAsync();
    Task<string> JoinRide(int Id, string name, string phone);
}