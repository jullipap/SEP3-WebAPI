namespace Domain.Models;

public class Ride
{
    public int Id
    {
        get;
        set;
    }
    public Location StartLocation { get; set; }
    public Location Destination { get; set; }
    public DateTime StartDate { get; set; }
    public string Driver { get; set; }
    public int Capacity { get; set; }
    

    public Ride(Location destination, DateTime startDate, Location startLocation, int id, string driver , int capacity)
    {
        Destination = destination;
        StartDate = startDate;
        StartLocation = startLocation;
        Id = id;
        Driver = driver;
        Capacity = capacity;
    }

    // public bool isBetweenDates(DateTime value)
    // {
    //     return startDate.epoch <= value.epoch && value.epoch <= endDate.epoch;
    // }
}