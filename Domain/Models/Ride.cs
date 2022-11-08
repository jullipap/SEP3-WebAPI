namespace Domain.Models;

public class Ride
{
    public long Id
    {
        get;
        set;
    }
    public Location StartLocation { get; set; }
    public Location Destination { get; set; }
    public DateTime Date { get; set; }
    public string DriverName { get; set; }
    
    

    public bool isBetweenDates(DateTime value)
    {
        return Date.Epoch <= value.Epoch;
    }
}