using Base.Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Base.Infrastructure.Behaviors
{
    //Inicia uma transação automaticamente e faz Commit() ou Rollback() caso ocorra erro.
    //Agora todos os comandos que modificam o banco são transacionais, evitando dados inconsistentes.
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

        public TransactionBehavior(ApplicationDbContext context, ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("🔄 Iniciando transação para {RequestName}", typeof(TRequest).Name);

            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await next();
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                _logger.LogInformation("✅ Transação confirmada para {RequestName}", typeof(TRequest).Name);
                return response;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "❌ Erro! Transação revertida para {RequestName}", typeof(TRequest).Name);
                throw;
            }
        }
    }
}
