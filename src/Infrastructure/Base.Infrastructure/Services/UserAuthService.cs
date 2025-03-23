using Base.Application.Interfaces;
using Base.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Infrastructure.Services;

public class UserAuthService : IUserAuthService
{
    private readonly UserManager<User> _userManager;

    public UserAuthService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<(bool Success, string UserId, string Email, IList<string> Roles)> ValidateUserAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            return (false, "", "", []);

        var roles = await _userManager.GetRolesAsync(user);
        return (true, user.Id, user.Email, roles);
    }
}