using CarRental.Common.Enums;

namespace CarRental.Common.Classes;
public class Motorcycle : Vehicle
{
    public Motorcycle(string regNo, string make, int odometer, double costPerKilometer, VehicleTypes vehicleType, VehicleStatuses status, int id = default) : base(regNo, make, odometer, costPerKilometer, vehicleType, status, id) { }
    public Motorcycle() { }
}