using Base.Api.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection.PortableExecutable;

namespace Base.Api.Filters;

public class RequireHeaderFilter : IActionFilter
{
    private readonly string _headerName;

    //Filtra requisições que não tenham um header obrigatório, como X-Api-Key.
    public RequireHeaderFilter(string headerName)
    {
        _headerName = headerName;
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Headers.ContainsKey(_headerName))
        {
            var error = new ApiError(
                type: "ValidationError",
                error: "Cabeçalho obrigatório ausente",
                detail: $"O cabeçalho '{_headerName}' é obrigatório para esta requisição."
            );

            context.Result = new BadRequestObjectResult(error);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
