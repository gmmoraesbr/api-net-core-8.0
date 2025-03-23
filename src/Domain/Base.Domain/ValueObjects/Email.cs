namespace Base.Domain.ValueObjects;

public class Email
{
    public string Address { get; }
    protected Email() { } // For EF
    public Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address) || !address.Contains("@"))
            throw new ArgumentException("Invalid email", nameof(address));

        Address = address.ToLower();
    }
    public override bool Equals(object? obj) =>
        obj is Email other && Address == other.Address;
    public override int GetHashCode() => Address.GetHashCode();
    public override string ToString() => Address;
}
