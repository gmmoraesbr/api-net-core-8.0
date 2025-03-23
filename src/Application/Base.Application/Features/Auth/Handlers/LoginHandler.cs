using Base.Application.Features.Auth.Commands;
using Base.Application.Interfaces;
using Base.Domain.Contracts.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Base.Application.Features.Auth.Handlers;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUserAuthService _authService;
    private readonly IJwtService _jwtService;

    public LoginHandler(IUserAuthService authService, IJwtService jwtService)
    {
        _authService = authService;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var (success, userId, email, roles) = await _authService.ValidateUserAsync(request.Username, request.Password);

        if (!success)
            throw new UnauthorizedAccessException("Usuário ou senha inválidos.");

        var token = _jwtService.GenerateToken(userId, email, roles);

        return new LoginResponse
        {
            Token = token,
            Username = email,
            Roles = roles
        };
    }
}