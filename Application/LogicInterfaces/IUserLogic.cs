using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> Register(RegisterDto dto);
    Task<User> Login(LoginDto dto);

    Task<User> GetUserByIdAsync(int id);
    Task<User> UpdateTheLicenseNo(UpdateLicenseDto dto);
}