using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System.Diagnostics;

namespace Base.Application.Behaviors
{
    //Registra logs antes e depois da execução do Handler.
    //Todos os commands e queries serão logados, ajudando no debug e monitoramento.
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = request.GetType().Name;
            _logger.LogInformation("📢 Iniciando {RequestName} com dados: {@Request}", requestName, request);

            var response = await next();

            _logger.LogInformation("✅ {RequestName} processado com resposta: {@Response}", requestName, response);
            return response;
        }
    }

}
