using Base.Application.Common;
using Base.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Base.Application.Behaviors
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<AuthorizationBehavior<TRequest, TResponse>> _logger;
        private readonly ICurrentUserService _currentUserService;

        public AuthorizationBehavior(
            ILogger<AuthorizationBehavior<TRequest, TResponse>> logger,
            ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // 💡 Se `user` for nulo, significa que o request não veio de um contexto HTTP.
            if (request is not IAllowAnonymousRequest && !_currentUserService.IsAuthenticated)
            {
                _logger.LogWarning("❌ Acesso negado. Usuário não autenticado.");
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }

            if (request is IRequireRole requiredRoleRequest)
            {
                if (_currentUserService.Role != requiredRoleRequest.RequiredRole)
                {
                    _logger.LogWarning("❌ Role incorreta: {Role}", requiredRoleRequest.RequiredRole);
                    throw new UnauthorizedAccessException("Usuário não tem permissão.");
                }
            }

            return await next();
        }
    }

    public interface IRequireRole
    {
        string RequiredRole { get; }
    }
}
