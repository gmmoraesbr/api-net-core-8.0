using Base.Application.DTOs;
using Base.Application.Features.Orders.Requests;
using Base.Domain.ValueObjects;
using MediatR;

namespace Base.Application.Features.Orders.Commands;

public class CreateOrderCommand : IRequest<Guid>
{
    public Guid CustomerId { get; set; }
    public AddressDto ShippingAddress { get; set; } = null!;
    public List<CreateOrderItemRequest> Items { get; set; } = new();
}