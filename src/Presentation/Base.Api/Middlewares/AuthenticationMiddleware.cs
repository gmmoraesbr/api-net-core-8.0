using Base.Api.Common;
using System.Text.Json;

namespace Base.Api.Middlewares
{
    //Valida se o usuário está autenticado antes de acessar a API.
    //Garante que apenas usuários autenticados possam acessar a API.
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value;

            string[] publicPaths =
            [
                "/api/auth/login",
                "/api/auth/register"
            ];

            bool isPublic = publicPaths.Any(p => path.StartsWith(p, StringComparison.OrdinalIgnoreCase));

            if (!isPublic && !(context.User.Identity?.IsAuthenticated ?? false))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var error = new ApiError(
                    type: "AuthenticationError",
                    error: "Acesso não autorizado",
                    detail: "Você precisa estar autenticado para acessar este recurso."
                );

                var json = JsonSerializer.Serialize(error);
                await context.Response.WriteAsync(json);
                return;
            }

            await _next(context);
        }

    }

}
