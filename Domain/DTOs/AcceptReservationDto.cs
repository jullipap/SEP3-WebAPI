using Domain.Models;

namespace Domain.DTOs;

public class AcceptReservationDto
{
    public int RideId { get; set; }
    public bool DidAccept { get; set; }
}