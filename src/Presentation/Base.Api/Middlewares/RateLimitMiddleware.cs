using Base.Api.Common;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.Contracts;
using System.Text.Json;

namespace Base.Api.Middlewares
{
    //Limita o número de requisições para evitar sobrecarga no servidor.
    //Protege contra ataques DDoS e acessos excessivos.
    public class RateLimitMiddleware
    {
        private static readonly Dictionary<string, DateTime> _requests = new();
        private readonly RequestDelegate _next;

        public RateLimitMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            if (ipAddress != null)
            {
                if (_requests.TryGetValue(ipAddress, out var lastRequest) &&
                    (DateTime.UtcNow - lastRequest).TotalSeconds < 1)
                {
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    context.Response.ContentType = "application/json";

                    var error = new ApiError(
                        type: "RateLimitExceeded",
                        error: "Muitas requisições",
                        detail: "Você excedeu o número de requisições permitidas por segundo. Tente novamente mais tarde."
                    );

                    var json = JsonSerializer.Serialize(error);
                    await context.Response.WriteAsync(json);
                    return;
                }

                _requests[ipAddress] = DateTime.UtcNow;
            }

            await _next(context);
        }

    }

}
