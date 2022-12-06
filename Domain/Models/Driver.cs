using System.Collections;
using System.Xml.Serialization;

namespace Domain.Models;

public class Driver
//Refactor to User
{
    public string Name { get; set; }
    public List<Ride>? Rides { get; set; }

    public int? LicenseNumber { get; set; }
    public string Email { get; set; }
    public int Id { get; set; }
    public int Phone { get; set; }

    public Driver(string name, int? licenseNumber, string email, int id, int phone)
    {
        Name = name;
        LicenseNumber = licenseNumber;
        Email = email;
        Id = id;
        Phone = phone;
    }

    public Driver()
    {

    }
}