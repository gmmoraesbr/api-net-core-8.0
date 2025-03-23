namespace Base.Domain.ValueObjects;

public class Cpf
{
    public string Number { get; }

    protected Cpf() { } // For EF

    public Cpf(string number)
    {
        if (!ValidateCpf(number))
            throw new ArgumentException("Invalid CPF", nameof(number));

        Number = number;
    }

    private static bool ValidateCpf(string cpf)
    {
        cpf = cpf.Replace(".", "").Replace("-", "");
        if (cpf.Length != 11 || cpf.Distinct().Count() == 1) return false;
        return true; // Real CPF validation implementation
    }
}