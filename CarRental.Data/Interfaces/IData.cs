using CarRental.Common.Enums;
using CarRental.Common.Interfaces;
using System.Linq.Expressions;

namespace CarRental.Data.Interfaces;
public interface IData
{
    int NextVehicleId { get; }
    int NextPersonId { get; }
    int NextBookingId { get; }
    string[] VehicleStatusNames { get; }
    string[] VehicleTypeNames { get; }

    #region Generic Methods
    List<T> Get<T>(Func<T, bool>? expression) where T : class;
    T? Single<T>(Func<T, bool>? expression) where T : class;
    void Add<T>(T item) where T : class;
    #endregion

    #region Non-generic methods
    void RentVehicle(int vehicleId, int customerId);
    IBooking? ReturnVehicle(int vehicleId);
    VehicleTypes GetVehicleType(string name);
    #endregion
}
