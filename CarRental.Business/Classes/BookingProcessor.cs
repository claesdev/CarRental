using CarRental.Common.Classes;
using CarRental.Common.Enums;
using CarRental.Common.Extensions;
using CarRental.Common.Interfaces;
using CarRental.Data.Interfaces;

namespace CarRental.Business.Classes;
public class BookingProcessor
{
    private readonly IData _db;

    public bool _isDisabled = false;
    public bool _isHidden = true;
    public int SelectedCustomerID { get; set; }
    public string AlertMessage { get; set; } = string.Empty;
    public bool IsAlertMessageHidden { get; set; } = true;

    public BookingProcessor(IData db) => _db = db;

    /// <summary>
    ///  TODO: Fix the bug
    /// </summary>
    /// <returns></returns>
    public IEnumerable<IBooking> GetBookings()
    {
        var bookings = _db.Get<IBooking>(b => b.Id >= 0);
        
        foreach (var booking in bookings)
        {
            if (booking.Status == "Open")
            {
                booking.KmReturned = null;
                booking.Returned = default;
                booking.Cost = null;
            }
            else if (booking.Status == "Closed")
            {

            }
        }

        return _db.Get<IBooking>(b => b.Id >= 0);
    }

    public IEnumerable<IPerson> GetCustomers() => _db.Get<IPerson>(p => p.Id >= 0);

    public IPerson? GetPerson(int customerId) => _db.Single<IPerson>(p => p.Id == customerId);

    public IPerson? GetPerson(string ssn) =>
        _db.Single<IPerson>(p => p.Ssn == ssn);

    public IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default) =>
        _db.Get<IVehicle>(v => v.Id >= 0);

    public IVehicle? GetVehicle(int vehicleId) => _db.Single<IVehicle>(v => v.Id == vehicleId);

    public IVehicle? GetVehicle(string regNo) => _db.Single<IVehicle>(v => v.RegNo == regNo);

    public async Task RentVehicle(int vehicleId, int customerId)
    { 
        _isDisabled = true;
        _isHidden = false;
        await Task.Delay(3000);
        _isDisabled = false;
        _isHidden = true;

        _db.RentVehicle(vehicleId, customerId);
    }

    public void ReturnVehicle(int vehicleId, double distance)
    {
        try
        {
            var booking = _db.ReturnVehicle(vehicleId);
            if (booking is null)
                throw new NullReferenceException(nameof(booking));

            booking.KmReturned = booking.KmRented + distance;
            booking.Returned = DateTime.Today;
            var duration = booking.Rented.Duration(booking.Returned);

            var vehicle = _db.Single<IVehicle>(v => v.Id == vehicleId + 1) ?? throw new NullReferenceException($"Vehicle {vehicleId}");
            vehicle.Status = VehicleStatuses.Available;
            vehicle.Distance = default;
            
            booking.Cost = duration * vehicle?.CostPerDay + distance * vehicle?.CostPerKilometer;
            booking.Status = "Closed";
        }
        catch (NullReferenceException)
        {
            AlertMessage = "Couldn't return vehicle.";
        }
        
    }

    public void AddCustomer(Customer customer)
    {
        if (customer.Ssn == string.Empty || customer.FirstName == string.Empty || customer.LastName == string.Empty)
        {
            AlertMessage = "Couldn't add customer. Please fill out all forms before submitting.";
            IsAlertMessageHidden = false;

            customer.Ssn = string.Empty;
            customer.LastName = string.Empty;
            customer.FirstName = string.Empty;

            return;
        }
        _db.Add<IPerson>(new Customer(customer.Ssn, customer.LastName, customer.FirstName, _db.NextPersonId));

        if (!IsAlertMessageHidden) 
            IsAlertMessageHidden = true;

        customer.Ssn = string.Empty;
        customer.LastName = string.Empty;
        customer.FirstName = string.Empty;
    }

    public void AddVehicle(Vehicle vehicle)
    {
        if (vehicle.RegNo == string.Empty ||
            vehicle.Make == string.Empty ||
            vehicle.Odometer == default ||
            vehicle.CostPerKilometer == default ||
            vehicle.VehicleType == 0)
        {
            AlertMessage = "Couldn't add vehicle to list. Please fill out all forms before submitting.";
            IsAlertMessageHidden = false;

            vehicle.RegNo = string.Empty;
            vehicle.Make = string.Empty;
            vehicle.Odometer = 0;
            vehicle.CostPerKilometer = 0;
            vehicle.VehicleType = 0;

            return;
        }
        _db.Add<IVehicle>(new Vehicle(vehicle.RegNo, vehicle.Make, vehicle.Odometer,vehicle.CostPerKilometer, vehicle.VehicleType, id: _db.NextVehicleId));

        if (!IsAlertMessageHidden) 
            IsAlertMessageHidden = true;

        vehicle.RegNo = string.Empty;
        vehicle.Make = string.Empty;
        vehicle.Odometer = 0;
        vehicle.CostPerKilometer = 0;
        vehicle.VehicleType = 0;        
    }

    public string[] VehicleStatusNames => _db.VehicleStatusNames;
    public string[] VehicleTypeNames => _db.VehicleTypeNames;
    public VehicleTypes GetVehicleType(string name) => _db.GetVehicleType(name);
}
