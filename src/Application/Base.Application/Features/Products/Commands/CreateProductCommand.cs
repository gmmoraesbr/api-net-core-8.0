using Base.Application.DTOs;
using Base.Application.Features.Products.Responses;
using MediatR;

namespace Base.Application.Features.Products.Commands;

public class CreateProductCommand : IRequest<ProductResponse>
{
    public string Title { get; init; } = default!;
    public decimal Price { get; init; }
    public string Description { get; init; } = default!;
    public string Category { get; init; } = default!;
    public string Image { get; init; } = default!;
    public RatingDto Rating { get; init; } = default!;
}