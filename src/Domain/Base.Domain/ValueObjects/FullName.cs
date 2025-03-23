namespace Base.Domain.ValueObjects;

public class FullName
{
    public string FirstName { get; }
    public string LastName { get; }

    protected FullName() { } // For EF

    public FullName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Invalid full name");

        FirstName = firstName;
        LastName = lastName;
    }

    public override string ToString() => $"{FirstName} {LastName}";
}
