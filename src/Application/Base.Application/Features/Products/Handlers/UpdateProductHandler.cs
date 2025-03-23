using AutoMapper;
using Base.Application.Common;
using Base.Application.Features.Products.Commands;
using Base.Application.Features.Products.Responses;
using Base.Domain.Contracts.Repositories;
using Base.Domain.Entities.Aggregates.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Base.Application.Features.Products.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        private readonly DomainEventDispatcher _domainEventDispatcher;

        public UpdateProductHandler(IProductRepository repo, IMapper mapper, DomainEventDispatcher domainEventDispatcher)
        {
            _repo = repo;
            _mapper = mapper;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repo.Query().FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product == null)
                throw new KeyNotFoundException($"Produto com ID {request.Id} não encontrado.");

            product.Update(request.Title, request.Description, request.Category, request.Image);
            product.AlterPrice(request.Price);
            product.Rating.Update(request.Rating.Rate, request.Rating.Count);

            await _repo.UpdateAsync(product);
            await _domainEventDispatcher.DispatchEventsAsync(product);

            return _mapper.Map<ProductResponse>(product);
        }

    }
}
