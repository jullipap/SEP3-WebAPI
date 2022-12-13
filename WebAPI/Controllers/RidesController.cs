using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [HttpPost, Route("reservation")]
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
    [Authorize]

    public async Task<ActionResult<List<Ride>>> GetAllAsync(string userId, string? startDate = null, string? endDate = null)
    {
        try
        {
            var rides = await rideLogic.GetAllAsync(startDate, endDate, userId);
            return Ok(rides);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [Authorize]
    [HttpPost]
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
    [Authorize]
    [HttpGet, Route("driver/{driverId:int}")]
    public async Task<ActionResult<List<Ride>>> GetRidesByDriverId([FromRoute]int driverId)
    {
        try
        {
            var rides = await rideLogic.GetRidesByDriverId(driverId);
            return Ok(rides);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Ride>> GetRideById([FromRoute] int id)
    {
        try
        {
            Ride ride = await rideLogic.GetRideByIdAsync(id);
            return Ok(ride);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [Authorize]
    [HttpPatch]
    public async Task<ActionResult> ChangeRideStatus([FromBody]ChangeStatusDto dto)
    {
        try
        {
            await rideLogic.ChangeRideStatusAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}