using CarRental.Common.Enums;
using CarRental.Common.Interfaces;

namespace CarRental.Common.Classes;

public class Vehicle : IVehicle
{
    public string RegNo { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public int Odometer { get; set; } = 0;
    public double CostPerKilometer { get; set; } = 0.0;
    public VehicleTypes VehicleType { get; set; } = default;
    public double CostPerDay { get; init; } = 0.0;
    public VehicleStatuses Status { get; set; }
    public int Id { get; set; }
    public int Distance { get; set; } = 0;

    public Vehicle(string regNo, string make, int odometer, double costPerKilometer, VehicleTypes vehicleType, VehicleStatuses status = VehicleStatuses.Available, int id = default)
    {
        RegNo = regNo;
        Make = make;
        Odometer = odometer;
        CostPerKilometer = costPerKilometer;
        VehicleType = vehicleType;
        // TODO: Set CostPerDay to value of vehicleType.
        CostPerDay = (double)VehicleType;
        Status = status;
        Id = id;
    }
    public Vehicle() { }
}
