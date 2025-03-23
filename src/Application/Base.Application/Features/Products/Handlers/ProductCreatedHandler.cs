using Base.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.Features.Products.Handlers;

public class ProductCreatedHandler : INotificationHandler<ProductCreatedEvent>
{
    public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Produto criado com ID: {notification.Product.Id}");

        return Task.CompletedTask;
    }
}