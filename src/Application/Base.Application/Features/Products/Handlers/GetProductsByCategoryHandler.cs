using AutoMapper;
using Base.Application.Common.Pagination;
using Base.Application.Features.Products.Responses;
using Base.Domain.Contracts.Repositories;
using Base.Domain.Entities.Aggregates.Product;
using MediatR;
using System.Linq.Dynamic.Core;

namespace Base.Application.Features.Products.Handlers
{
    public class GetProductsByCategoryHandler : IRequestHandler<GetProductsByCategoryQuery, PaginatedProductResponse>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public GetProductsByCategoryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PaginatedProductResponse> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var query = _repo.Query()
                             .Where(p => p.Category.ToLower() == request.Category.ToLower());

            // Ordering
            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                query = query.OrderBy(request.OrderBy);
            }

            var paginated = await PaginatedList<Product>.CreateAsync(query, request.Page, request.Size);

            return new PaginatedProductResponse
            {
                Data = _mapper.Map<List<ProductResponse>>(paginated.Items),
                TotalItems = paginated.TotalItems,
                CurrentPage = paginated.CurrentPage,
                TotalPages = paginated.TotalPages
            };
        }
    }
}
