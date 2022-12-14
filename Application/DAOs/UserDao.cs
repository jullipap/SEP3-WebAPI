using System.Configuration;
using Application.DaoInterfaces;
using Domain.Models;
using Grpc.Net.Client;

namespace Application.DAOs;
// its red cause there is no dao interface yet 
public class UserDao : IUserDao
{
    private Users.UsersClient client;

    public UserDao()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5434");
        client = new Users.UsersClient(channel);
    }

    public  async Task<User> Register(string name, int phone, string email, string encryptedPassword, int licenseNo)
    {
        CreateAccountMessage createAccountMessage;
        if (licenseNo == -1)
        {
            createAccountMessage = new CreateAccountMessage() 
            { 
                Name = name,
                PhoneNumber = phone,
                Email = email,
                Password = encryptedPassword
            
            };
        }
        else
        {
            createAccountMessage = new CreateAccountMessage() 
            { 
                Name = name,
                LicenseNo = licenseNo,
                PhoneNumber = phone,
                Email = email,
                Password = encryptedPassword
            
            };
        }
        var reply = await client.createAccountAsync(createAccountMessage);
        
        User user = new User()
        {
            Name = reply.Name,
            Email = reply.Email,
            Phone = reply.PhoneNumber
        };
        return user;
    }

    public  async Task<User> Login(string email, string password)
    {
        LoginMessage loginMessage = new LoginMessage() { Email = email, Password = password};
        var reply = await client.loginAsync(loginMessage);
        if (!reply.Status)
        {
            throw new Exception("Incorrect data, try to log in again");
        }

        int? licenseNo = null;
        if (reply.HasLicenseNo)
        {
            licenseNo = reply.LicenseNo;
        }
        User user = new User()
        {
            Id = reply.UserId,
            LicenseNumber = licenseNo
        };
        return user;
    }

    public  async Task<User> GetUserByIdAsync(int id)
    {
        UsersMessageId userMessageId = new UsersMessageId() { UserId = id };
        var reply = await client.getDriverAsync(userMessageId);
        User user = new User()
        {
            Name = reply.Name,
            Email = reply.Email,
            Phone = reply.PhoneNumber
        };
        return user;
    }

    public async Task UpdateTheLicenseNo(int id, int licenseNo)
    {
        LicenseMessage licenseMessage = new LicenseMessage()
        {
            LicenseNo = licenseNo,
            UserId = id
        };
        
        var reply = await client.updateLicenseAsync(licenseMessage);
        
        if (!reply.Status)
        {
            throw new Exception("Updating license resulted in failure");
        }
    }
}