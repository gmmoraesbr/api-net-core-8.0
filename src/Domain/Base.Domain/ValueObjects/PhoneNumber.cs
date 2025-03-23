namespace Base.Domain.ValueObjects;

public class PhoneNumber
{
    public string AreaCode { get; }
    public string Number { get; }

    protected PhoneNumber() { } // For EF

    public PhoneNumber(string areaCode, string number)
    {
        if (string.IsNullOrWhiteSpace(areaCode) || string.IsNullOrWhiteSpace(number) || number.Length < 8)
            throw new ArgumentException("Invalid phone number");

        AreaCode = areaCode;
        Number = number;
    }

    public override string ToString() => $"({AreaCode}) {Number}";
}