using Base.Api.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Base.Api.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;

    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "❌ Unhandled exception");

        // Define o status apropriado com base no tipo da exceção
        var (statusCode, errorResponse) = context.Exception switch
        {
            ArgumentException => (
                StatusCodes.Status400BadRequest,
                new ApiError("ValidationError", "Requisição inválida", context.Exception.Message)
            ),
            UnauthorizedAccessException => (
                StatusCodes.Status401Unauthorized,
                new ApiError("AuthenticationError", "Acesso não autorizado", context.Exception.Message)
            ),
            KeyNotFoundException => (
                StatusCodes.Status404NotFound,
                new ApiError("ResourceNotFound", "Recurso não encontrado", context.Exception.Message)
            ),
            _ => (
                StatusCodes.Status500InternalServerError,
                new ApiError("InternalServerError", "Erro interno no servidor", context.Exception.Message)
            )
        };

        context.Result = new ObjectResult(errorResponse)
        {
            StatusCode = statusCode
        };

        context.ExceptionHandled = true;
    }

}
