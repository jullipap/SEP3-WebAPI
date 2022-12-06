﻿using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public Task<User> Register(RegisterDto dto)
    {
        string encryptedPassword = GetHashString(dto.Password);
        return userDao.Register(dto.Name, Int32.Parse( dto.Phone), dto.Email, encryptedPassword, Int32.Parse(dto.LicenseNumber));
    }

    public Task<User> Login(LoginDto dto)
    {
        string encryptedPassword = GetHashString(dto.Password);
        return userDao.Login(dto.Email, encryptedPassword);
    }

    public Task<User> GetUserByIdAsync(int id)
    {
        return userDao.GetUserByIdAsync(id);
    }

    public Task<User> UpdateTheLicenseNo(UpdateLicenseDto dto)
    {
        return userDao.UpdateTheLicenseNo(dto.UserId, dto.LicenseNo) as Task<User>;
        //this is a safe cast. There was some error and it might solve it
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