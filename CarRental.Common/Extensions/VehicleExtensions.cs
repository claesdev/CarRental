namespace CarRental.Common.Extensions;

public static class VehicleExtensions
{

    /// TODO: Implement extension method
    public static int Duration(this DateTime startDate, DateTime endDate) => 
        endDate.DayOfYear == startDate.DayOfYear ? 1 : endDate.DayOfYear - startDate.DayOfYear;

}
