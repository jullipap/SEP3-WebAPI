﻿using Application.DAOs;
using Domain.Models;
using DateTime = Domain.Models.DateTime;

namespace Application.DaoInterfaces;

public interface IRideDao
{
    Task<List<Ride>> GetAllAsync(DateTime? startDate, DateTime? endDate);
    Task<string> JoinRide(int userId, int rideId);

    Task<Ride> CreateRide(Location startLocation, Location destination, DateTime startDate, int driverId,
        int capacity);
}