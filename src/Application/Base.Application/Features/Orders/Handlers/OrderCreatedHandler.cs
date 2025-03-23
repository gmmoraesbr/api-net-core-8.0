using Base.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.Features.Orders.Handlers;
public class OrderCreatedHandler : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        // Lógica que deve acontecer após o pedido ser criado
        Console.WriteLine($"Pedido criado com ID: {notification.Order.Id}");

        return Task.CompletedTask;
    }
}