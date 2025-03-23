
using Microsoft.AspNetCore.Identity;

namespace Base.Domain.Entities;

public class User : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public override string? UserName { get; set; } = string.Empty;
    public bool Active { get; private set; } = true;

    public ICollection<UserClaim> Claims { get; set; } = new List<UserClaim>();
    public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();

    protected User() { }

    private User(string name, string username, string email)
    {
        Name = name;
        UserName = username;
        Email = email;
    }

    public static User Create(string name, string username, string email)
    {
        var user = new User(name, username, email);

        return user;
    }

    public void Deactivate() => Active = false;
    public void Activate() => Active = true;
}

