using Base.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.Features.Auth.Commands;

public class LoginCommand : IRequest<LoginResponse>, IAllowAnonymousRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public IList<string> Roles { get; set; } = Array.Empty<string>();
}