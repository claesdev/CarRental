using CarRental.Common.Enums;

namespace CarRental.Common.Interfaces;
public interface IVehicle
{
    int Id { get; set; }
    string RegNo { get; set; }
    string Make { get; set; }
    int Odometer { get; set; }
    double CostPerKilometer { get; set; }
    VehicleTypes VehicleType { get; set; }
    double CostPerDay { get; init; }
    VehicleStatuses Status { get; set; }
    int Distance {  get; set; }
}