﻿using CinemaTicketing.Domain.Screenings;

namespace CinemaTicketing.Domain.Theaters;

public class Room
{
    public int Id { get; set; }
    public int Capacity { get; set; }
    public int Rows { get; set; }
    public int Columns { get; set; }

    public int TheaterId { get; set; } = new();
    public List<Seat> Seats { get; set; } = new();
    public List<Screening> Screenings { get; set; } = new();
}