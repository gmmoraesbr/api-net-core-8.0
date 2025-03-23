using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.Interfaces;

public interface IUserAuthService
{
    Task<(bool Success, string UserId, string Email, IList<string> Roles)> ValidateUserAsync(string email, string password);
}
