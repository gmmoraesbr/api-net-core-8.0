using Microsoft.AspNetCore.Identity;

namespace Base.Infrastructure.Security;

public static class PasswordHasher
{
    private static readonly PasswordHasher<string> _hasher = new();

    public static string Hash(string password)
    {
        return _hasher.HashPassword(null!, password);
    }

    public static bool Verify(string hashedPassword, string inputPassword)
    {
        var result = _hasher.VerifyHashedPassword(null!, hashedPassword, inputPassword);
        return result == PasswordVerificationResult.Success;
    }
}
