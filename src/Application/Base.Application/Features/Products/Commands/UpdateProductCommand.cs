using Base.Application.DTOs;
using Base.Application.Features.Products.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.Features.Products.Commands
{
    public class UpdateProductCommand : IRequest<ProductResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public decimal Price { get; set; }
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public string Image { get; set; } = default!;
        public RatingDto Rating { get; set; } = default!;
    }
}
