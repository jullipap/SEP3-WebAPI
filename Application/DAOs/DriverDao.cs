using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Domain.Models;
using Grpc.Net.Client;

namespace Application.DAOs;

public class DriverDao : IDriverDao
{
    private User.UserClient client;

    public DriverDao()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5434");
        client = new User.UserClient(channel);
    }

    public async Task<Driver> Register(string name, int phone, string email, string encryptedPassword, int licenseNo)
    {
        
        CreateAccountMessage createAccountMessage = new CreateAccountMessage()
        {
            Name = name,
            Email = email,
            LicenseNo = licenseNo,
            Password = encryptedPassword,
            PhoneNumber = phone
        };
        
        var reply = await client.createAccountAsync(createAccountMessage);

        List<Ride> rides = new List<Ride>();

        Driver driver = new Driver(reply.Name)
        {
            Name = reply.Name,
            Rides = rides
        };
        // shouldnt we also have access to the ridesDao ?
        return driver;

    }

    public async Task<Driver> Login(string name, string password)
    {
        // rpc login(LoginMessage) returns (BoolMessage);
        // rpc getDriver(DriverMessage) returns (UserMessage);
       DriverMessage loginDto = new DriverMessage()
       {
          
       }
        
    }
}