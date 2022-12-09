using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationLogic reservationLogic;

    public ReservationsController(IReservationLogic reservationLogic)
    {
        this.reservationLogic = reservationLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Reservation>> AcceptPassenger([FromBody]AcceptReservationDto acceptDto)
    {
        try
        {
            var reservation = await reservationLogic.AcceptPassenger(acceptDto);
            return Ok(reservation);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet,Route("driver/{id:int}")]
    public async Task<ActionResult<ICollection<Reservation>>> GetReservationsToAccept([FromRoute] int id)
    {
        try
        {
            var reservations = await reservationLogic.GetReservationToAccept(id);
            return Ok(reservations);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet, Route("ride/{rideId:int}")]
    public async Task<ActionResult<List<Reservation>>> GetAcceptedReservationsByRideId([FromRoute]int rideId)
    {
        try
        {
            var reservations = await reservationLogic.GetAcceptedReservationsByRideId(rideId);
            return Ok(reservations);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}