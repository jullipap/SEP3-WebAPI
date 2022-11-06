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
            Ride created = await rideLogic.JoinRide(dto);
            return Created("/", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
        
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Ride>>> GetAllAsync()
    {
        try
        {
            var rides = await rideLogic.getAllAsync();
            return Ok(rides);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}