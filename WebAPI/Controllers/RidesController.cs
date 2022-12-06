using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RidesController : ControllerBase
{
    private readonly IRideLogic rideLogic;

    public RidesController(IRideLogic rideLogic)
    {
        this.rideLogic = rideLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Ride>> JoinRide([FromBody] JoinRideDto dto)
    {
        try
        {
            string created = await rideLogic.JoinRide(dto);
            return Ok(created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
        
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Ride>>> GetAllAsync(string? startDate = null, string? endDate = null)
    {
        try
        {
            var rides = await rideLogic.GetAllAsync(startDate, endDate);
            return Ok(rides);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("Create")]
    public async Task<ActionResult<Ride>> createRide([FromBody] RideCreationDto dto)
    {
        try
        {
            var ride = await rideLogic.CreateRide(dto);
            //wanted to do Created(ride) but doesn't accept the parameter, we could do:
            //Created("rides/${id}") but we don't have that one yet
            return Ok(ride);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}