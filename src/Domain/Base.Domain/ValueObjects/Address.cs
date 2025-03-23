namespace Base.Domain.ValueObjects;

public class Address
{
    public string Street { get; }
    public string Number { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }

    protected Address() { } // For EF

    public Address(string street, string number, string city, string state, string zipCode)
    {
        if (string.IsNullOrWhiteSpace(street) || string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(state) || string.IsNullOrWhiteSpace(zipCode))
            throw new ArgumentException("Invalid address");

        Street = street;
        Number = number;
        City = city;
        State = state;
        ZipCode = zipCode;
    }
}