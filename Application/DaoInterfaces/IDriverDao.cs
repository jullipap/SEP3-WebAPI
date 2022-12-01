using Domain.Models;

namespace Application.DaoInterfaces;

public interface IDriverDao
{
    Task<Driver> Register(string name, int phone, string email, string encryptedPassword, int licenseNo);
    Task<Driver> Login(string name, string password);
}