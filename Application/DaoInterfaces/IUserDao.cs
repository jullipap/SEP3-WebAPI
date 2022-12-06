using Domain.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> Register(string name, int phone, string email, string encryptedPassword, int licenseNo);
    Task<User> Login(string name, string password);
    Task<User> GetUserByIdAsync(int id);

    Task UpdateTheLicenseNo(int id, int licenseNo);

}