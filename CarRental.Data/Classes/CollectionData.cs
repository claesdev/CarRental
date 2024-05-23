using CarRental.Common.Classes;
using CarRental.Common.Enums;
using CarRental.Common.Extensions;
using CarRental.Common.Interfaces;
using CarRental.Data.Interfaces;

namespace CarRental.Data.Classes;
public class CollectionData : IData
{
    readonly List<IPerson> _persons = new List<IPerson>();
    readonly List<IVehicle> _vehicles = new List<IVehicle>();
    readonly List<IBooking> _bookings = new List<IBooking>();
    public int NextVehicleId => _vehicles.Count.Equals(0) ? 1 : _vehicles.Max(v => v.Id) + 1;

    public int NextPersonId => _persons.Count.Equals(0) ? 1 : _persons.Max(p => p.Id) + 1;

    public int NextBookingId => _bookings.Count.Equals(0) ? 1 : _bookings.Max(b => b.Id) + 1;

    public string[] VehicleStatusNames => Enum.GetNames(typeof(VehicleStatuses));

    public string[] VehicleTypeNames => Enum.GetNames(typeof(VehicleTypes));

    public CollectionData() => SeedData();
    void SeedData()
    {
        // TODO: Implement this method.
        // Lägger till data till listorna
        _persons.Add(new Customer("12345", "Doe", "John", NextPersonId));
        _persons.Add(new Customer("98765", "Doe", "Jane", NextPersonId));

        _vehicles.Add(new Car("ABC123", "Volvo", 10000, 1,   VehicleTypes.Combi, VehicleStatuses.Available, NextVehicleId));
        _vehicles.Add(new Car("GHI789", "Tesla", 1000,  3,   VehicleTypes.Sedan, VehicleStatuses.Booked,    NextVehicleId));
        _vehicles.Add(new Car("DEF456", "Saab",  20000, 1,   VehicleTypes.Sedan, VehicleStatuses.Available, NextVehicleId));
        _vehicles.Add(new Car("JKL012", "Jeep",  5000,  1.5, VehicleTypes.Van,   VehicleStatuses.Available, NextVehicleId));
        _vehicles.Add(new Motorcycle("MNO234", "Yamaha", 30000, 0.5, VehicleTypes.Motorcycle, VehicleStatuses.Available, NextVehicleId));

        _bookings.Add(new Booking("GHI789", new Customer("12345", "Doe", "John"), 1000, default, new DateTime(2023, 9, 20), default, NextBookingId));
        
        var closedBooking = new Booking("JKL012", new Customer("98765", "Doe", "Jane"), 5000, 6500, new DateTime(2023, 9, 20), new DateTime(2023, 9, 27), NextBookingId, "Closed");
        var duration = closedBooking.Rented.Duration((DateTime)closedBooking.Returned);
        var vehicle = Single<IVehicle>(v => v.RegNo == closedBooking.RegNo);
        var distance = closedBooking.KmReturned - closedBooking.KmRented;
        closedBooking.Cost = duration * vehicle?.CostPerDay + distance * vehicle?.CostPerKilometer;
        _bookings.Add(closedBooking);
    }

    public List<T> Get<T>(Func<T, bool>? expression) where T : class
    {
        try
        {
            return GetType()
                .GetVaraibles()
                .FindCollection<T>()
                .GetData(this)
                .ToQueryable<T>()
                .Filter(expression);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public T? Single<T>(Func<T, bool>? expression) where T : class
    {
        try
        {
            return GetType()
                .GetVaraibles()
                .FindCollection<T>()
                .GetData(this)
                .ToQueryable<T>()
                .FilterSingle(expression);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public void Add<T>(T item) where T : class
    {
        if (item is IVehicle)
        {
            _vehicles.Add((IVehicle)item);
        }
        else if (item is IPerson)
        {
            _persons.Add((IPerson)item);
        }
        else if (item is IBooking)
        {
            _bookings.Add((IBooking)item);
        }
    }

    public void RentVehicle(int vehicleId, int customerId)
    {
        var vehicle = _vehicles[vehicleId];
        vehicle.Status = VehicleStatuses.Booked;
        var customer = _persons[customerId];
        _bookings.Add(new Booking(vehicle.RegNo, customer, vehicle.Odometer, rented: DateTime.Today, id: NextBookingId));
    }

    public IBooking? ReturnVehicle(int vehicleId)
    {
        var vehicle = _vehicles[vehicleId];
        var booking = _bookings.LastOrDefault(b => b.RegNo == vehicle.RegNo);
        if (booking == null) throw new NullReferenceException();
        return booking;
    }

    public VehicleTypes GetVehicleType(string name)
    {
        throw new NotImplementedException();
    }
}