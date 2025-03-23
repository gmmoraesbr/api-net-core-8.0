using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.Features.Products.Responses
{
    public class GetProductsQuery : IRequest<PaginatedProductResponse>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string? OrderBy { get; set; }
    }
}
