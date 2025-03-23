using Microsoft.Extensions.Options;
using Base.Api.Common;
using Base.Api.Configuration;
using System.Text.Json;

namespace Base.Api.Middlewares
{
    //Atribui um identificador único para rastrear cada requisição.
    //Ajuda a rastrear logs de requisição entre serviços distribuídos.
    public class CorrelationIdMiddleware
    {
        private const string CorrelationIdHeader = "X-Correlation-ID";
        private readonly RequestDelegate _next;
        private readonly CorrelationSettings _settings;

        public CorrelationIdMiddleware(RequestDelegate next, CorrelationSettings settings)
        {
            _next = next;
            _settings = settings;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId) ||
                string.IsNullOrWhiteSpace(correlationId) ||
                correlationId != _settings.ExpectedId)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var error = new ApiError(
                    type: "ValidationError",
                    error: "Cabeçalho inválido",
                    detail: $"O cabeçalho '{CorrelationIdHeader}' inválido'."
                );

                var json = JsonSerializer.Serialize(error);
                await context.Response.WriteAsync(json);
                return;
            }

            context.Response.Headers[CorrelationIdHeader] = correlationId;
            await _next(context);
        }
    }


}
