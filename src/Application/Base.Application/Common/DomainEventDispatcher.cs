using Base.Domain.Common;
using MediatR;
using System.Security.Cryptography;
namespace Base.Application.Common;

public class DomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task DispatchEventsAsync(BaseGuidEntity entity)
    {
        var events = entity.DomainEvents.ToList();
        entity.ClearDomainEvents();

        foreach (var domainEvent in events)
        {
            await _mediator.Publish(domainEvent);
        }
    }

    public async Task DispatchEventsAsync(BaseEntity<int> entity)
    {
        var events = entity.DomainEvents.ToList();
        entity.ClearDomainEvents();

        foreach (var domainEvent in events)
        {
            await _mediator.Publish(domainEvent);
        }
    }
}
