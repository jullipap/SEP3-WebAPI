using System.Xml;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
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
        return driverDao.Register(dto.name, dto.phone, dto.email, dto.password, dto.licenseNo);
    }

    public Task<Driver> Login(LoginDto dto)
    {
        return driverDao.Login(dto.name, dto.password);
    }
}