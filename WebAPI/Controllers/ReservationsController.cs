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

    [HttpGet]
    public async Task<ActionResult<ICollection<Reservation>>> GetReservationsToAccept()
    {
        try
        {
            var reservations = await reservationLogic.GetReservationToAccept();
            return Ok(reservations);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}