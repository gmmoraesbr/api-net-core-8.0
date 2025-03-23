using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Base.Application.Behaviors
{
    //Mede o tempo de execução dos Handlers e alerta se estiver demorando demais.
    //Ajuda a identificar requisições lentas e otimizar performance.
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;
        private readonly Stopwatch _timer;

        public PerformanceBehavior(ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
            _timer = new Stopwatch();
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;
            if (elapsedMilliseconds > 500) // Alerta se passar de 500ms
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogWarning("⚠️ {RequestName} demorou {ElapsedMilliseconds}ms", requestName, elapsedMilliseconds);
            }

            return response;
        }
    }

}
