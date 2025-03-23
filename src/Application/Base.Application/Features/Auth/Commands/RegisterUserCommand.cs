using Base.Application.Common;
using MediatR;

namespace Base.Application.Features.Auth.Commands
{
    public class RegisterUserCommand : IRequest<bool>, IAllowAnonymousRequest
    {
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        //public string Role { get; set; } = string.Empty;
    }
}
