using FluentValidation;
using MediatR;
using System;
using System.Diagnostics;

namespace Base.Application.Behaviors
{
    //Executa validações de FluentValidation antes de processar o comando.
    //Agora todos os comandos são validados automaticamente, sem precisar chamar Validate() manualmente em cada Handler.
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var errors = _validators
                    .Select(v => v.Validate(context))
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (errors.Any())
                {
                    throw new ValidationException(errors);
                }
            }

            return await next();
        }
    }

}
