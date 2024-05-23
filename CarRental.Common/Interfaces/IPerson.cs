namespace CarRental.Common.Interfaces;
public interface IPerson
{
    string Ssn { get; set; }
    string LastName { get; set; }
    string FirstName { get; set; }
    int Id { get; set; }
}
