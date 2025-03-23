using Base.Api.Common;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Base.Api.Middlewares
{
    //Captura erros globais e retorna respostas padronizadas em JSON.
    //Centraliza o tratamento de exceções, evitando a necessidade de try/catch em controllers.
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado.");

                context.Response.ContentType = "application/json";

                var (statusCode, errorResponse) = ex switch
                {
                    KeyNotFoundException => (
                        HttpStatusCode.NotFound,
                        new ApiError("ResourceNotFound", "Recurso não encontrado", ex.Message)
                    ),
                    UnauthorizedAccessException => (
                        HttpStatusCode.Unauthorized,
                        new ApiError("AuthenticationError", "Acesso não autorizado", ex.Message)
                    ),
                    ValidationException => (
                        HttpStatusCode.BadRequest,
                        new ApiError("ValidationError", "Dados inválidos", ex.Message)
                    ),
                    _ => (
                        HttpStatusCode.InternalServerError,
                        new ApiError("InternalServerError", "Ocorreu um erro interno no servidor", ex.Message)
                    )
                };

                context.Response.StatusCode = (int)statusCode;

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }

    }

}
