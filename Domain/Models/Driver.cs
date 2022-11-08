namespace Domain.Models;

public class Driver
{
    public string Name { get; set; }
    public Task <List<Ride>> Rides { get; set; }
}
