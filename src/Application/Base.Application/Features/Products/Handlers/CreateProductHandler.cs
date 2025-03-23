using Base.Application.Common;
using Base.Application.Features.Products.Commands;
using Base.Application.Features.Products.Responses;
using Base.Domain.Contracts.Repositories;
using Base.Domain.Entities.Aggregates.Product;
using MediatR;

namespace Base.Application.Features.Produtos.Handlers;

using AutoMapper;
using Base.Application.Features.Products.Commands;
using Base.Application.Features.Products.Responses;
using Base.Domain.Contracts.Repositories;
using Base.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _repo;
    private readonly DomainEventDispatcher _domainEventDispatcher;

    public CreateProductHandler(IMapper mapper, IProductRepository repo, DomainEventDispatcher domainEventDispatcher)
    {
        _mapper = mapper;
        _repo = repo;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(
            title: request.Title,
            price: request.Price,
            description: request.Description,
            category: request.Category,
            image: request.Image,
            rating: Rating.Create(0, request.Rating.Rate, request.Rating.Count) // ProductId = 0 por enquanto
        );

        await _repo.AddAsync(product);
        await _domainEventDispatcher.DispatchEventsAsync(product);

        return _mapper.Map<ProductResponse>(product);
    }
}

