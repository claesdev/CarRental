using CarRental.Common.Interfaces;

namespace CarRental.Common.Classes;
public class Booking : IBooking
{
    public string RegNo { get; init; }
    public IPerson? Customer { get; init; }
    public double KmRented { get; init; }
    public double? KmReturned { get; set; }
    public DateTime Rented { get; init; }
    public DateTime Returned { get; set; }
    public double? Cost { get; set; } = 0;
    public string Status { get; set; }
    public int Id { get; set; }

    public Booking(string regNo, IPerson customer, double kmRented, double kmReturned = 0, DateTime rented = default, DateTime returned = default, int id = default, string status = "Open")
    {
        RegNo = regNo;
        Customer = customer;
        KmRented = kmRented;
        KmReturned = kmReturned;
        Rented = rented;
        Returned = returned;
        Status = status;
    }
}
