using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IDriverLogic
{
    Task<Driver> Register(RegisterDto dto);
    Task<Driver> Login(LoginDto dto);

    Task<Driver> GetDriverByIdAsync(int id);
}