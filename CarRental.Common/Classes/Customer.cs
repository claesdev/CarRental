using CarRental.Common.Interfaces;

namespace CarRental.Common.Classes;
public class Customer : IPerson
{
    public string Ssn { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public int Id { get; set; }

    public Customer(string ssn = "", string lName = "", string fName = "", int id = default) => (Ssn, LastName, FirstName, Id) = (ssn, lName, fName, id);
}
