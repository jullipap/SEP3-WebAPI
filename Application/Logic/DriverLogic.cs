using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class DriverLogic : IDriverLogic
{
    private readonly IDriverDao driverDao;

    public DriverLogic(IDriverDao driverDao)
    {
        this.driverDao = driverDao;
    }

    public Task<Driver> Register(RegisterDto dto)
    {
        string encryptedPassword = GetHashString(dto.Password);
        return driverDao.Register(dto.Name, Int32.Parse( dto.Phone), dto.Email, encryptedPassword, Int32.Parse(dto.LicenseNumber));
    }

    public Task<Driver> Login(LoginDto dto)
    {
        string encryptedPassword = GetHashString(dto.Password);
        return driverDao.Login(dto.Email, encryptedPassword);
    }

    public Task<Driver> GetDriverByIdAsync(int id)
    {
        return driverDao.GetDriverByIdAsync(id);
    }

    private static byte[] GetHash(string inputString)
    {
        using (HashAlgorithm algorithm = SHA256.Create())
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }

    private static string GetHashString(string inputString)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in GetHash(inputString))
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }
}