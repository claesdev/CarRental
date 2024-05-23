using CarRental.Common.Interfaces;

namespace CarRental.Common.Classes;

public class NewEntities
{
    public Customer NewCustomer { get; set; } = new();
    public Vehicle NewVehicle { get; set; } = new();
}
