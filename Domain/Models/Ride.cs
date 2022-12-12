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
    public int DriverId { get; set; }
    public int Capacity { get; set; }
    public string Status { get; set; }

        public Ride(Location destination, DateTime startDate, Location startLocation, int id, int driverId , int capacity)
    {
        Destination = destination;
        StartDate = startDate;
        StartLocation = startLocation;
        Id = id;
        DriverId = driverId;
        Capacity = capacity;
        Status = "Created";
    }
    
    public Ride(){}

    // public bool isBetweenDates(DateTime value)
    // {
    //     return startDate.epoch <= value.epoch && value.epoch <= endDate.epoch;
    // }
}