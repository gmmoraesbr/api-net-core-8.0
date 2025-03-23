using Base.Application.Features.Auth.Commands;
using Base.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace Base.Application.Features.Auth.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<RegisterUserHandler> _logger;

        public RegisterUserHandler(UserManager<User> userManager, RoleManager<Role> roleManager, ILogger<RegisterUserHandler> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Username recebido: '{UserName}'", request.UserName);

            if (string.IsNullOrWhiteSpace(request.UserName))
                return false; // ou retornar erro de validação

            var user = User.Create(name: request.Name, username: request.UserName.Trim(), email: request.Email);

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                _logger.LogWarning("Falha ao criar usuário: {Erros}", string.Join(", ", result.Errors.Select(e => e.Description)));
                return false;
            }
            return true;
        }
    }
}
