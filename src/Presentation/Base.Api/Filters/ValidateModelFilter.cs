using Base.Api.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Base.Api.Filters;

public class ValidateModelFilter : IActionFilter
{
    //Garante que o ModelState seja válido antes de executar a action.
    //Se inválido, retorna 400 BadRequest automático.
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var validationMessages = context.ModelState
                .Where(e => e.Value?.Errors.Any() == true)
                .SelectMany(kv => kv.Value!.Errors.Select(e => $"{kv.Key}: {e.ErrorMessage}"))
                .ToList();

            var detail = string.Join(" | ", validationMessages);

            var error = new ApiError(
                type: "ValidationError",
                error: "Dados inválidos",
                detail: detail
            );

            context.Result = new BadRequestObjectResult(error);
        }
    }


    public void OnActionExecuted(ActionExecutedContext context) { }
}
