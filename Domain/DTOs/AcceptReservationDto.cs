﻿using Domain.Models;

namespace Domain.DTOs;

public class AcceptReservationDto
{
    public int ReservationId { get; set; }
    public bool DidAccept { get; set; }
}