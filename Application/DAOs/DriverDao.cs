using Domain.Models;
using Grpc.Net.Client;

namespace Application.DAOs;
// its red cause there is no dao interface yet 
public class DriverDao : IDriverDao
{
    private User.UserClient client;

    public DriverDao()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5434");
        client = new User.UserClient(channel);
    }

    public override async Task<Driver> Register(string name, int phone, string email, string encryptedPassword, int licenseNo)
    {
        CreateAccountMessage createAccountMessage = new CreateAccountMessage() 
        { 
            Name = name,
            LicenseNo = licenseNo,
            PhoneNumber = phone,
            Email = email,
            Password = encryptedPassword
            
        };
        var reply = await client.createAccountAsync(createAccountMessage);
        
        Driver driver = new Driver()
        {
            Name = reply.Name,
            Email = reply.Email,
            Phone = reply.PhoneNumber
        };
        return driver;
    }

    public override async Task<Driver> Login(string email, string password)
    {
        LoginMessage loginMessage = new LoginMessage() { Email = email, Password = password};
        var reply = await client.loginAsync(loginMessage);
        if (!reply.Status)
        {
            throw new Exception("Incorrect data, try to log in again");
        }
        Driver driver = new Driver()
        {
            Id = reply.DriverId,
        };
        return driver;
    }

    public override async Task<Driver> GetDriverByIdAsync(int id)
    {
        DriverMessageId driverMessageId = new DriverMessageId() { DriverId = id };
        var reply = await client.getDriverAsync(driverMessageId);
        Driver driver = new Driver()
        {
            Name = reply.Name,
            Email = reply.Email,
            Phone = reply.PhoneNumber
        };
        return driver;
    }
}