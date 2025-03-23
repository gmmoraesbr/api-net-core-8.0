using Base.Application.Common;
using Base.Application.Features.Orders.Commands;
using Base.Domain.Contracts.Repositories;
using Base.Domain.Entities.Aggregates.Order;
using Base.Domain.Events;
using Base.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace Base.Application.Features.Orders.Handlers;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMediator _mediator;
    private readonly DomainEventDispatcher _domainEventDispatcher;

    public CreateOrderHandler(IOrderRepository orderRepository, IMediator mediator, DomainEventDispatcher domainEventDispatcher)
    {
        _orderRepository = orderRepository;
        _mediator = mediator;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var address = new Address(
            request.ShippingAddress.Street,
            request.ShippingAddress.Number,
            request.ShippingAddress.City,
            request.ShippingAddress.State,
            request.ShippingAddress.ZipCode);

        var order = Order.Create(request.CustomerId, address);

        foreach (var item in request.Items)
        {
            var money = new Money(item.UnitPrice, item.Currency);
            order.AddItem(item.ProductId, item.ProductName, item.Quantity, money);
        }

        await _orderRepository.AddAsync(order);

        await _domainEventDispatcher.DispatchEventsAsync(order); // Dispara os eventos da entidade

        return order.Id;
    }
}