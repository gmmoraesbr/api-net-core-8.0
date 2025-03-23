using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.Features.Products.Responses
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public decimal Price { get; set; }
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public string Image { get; set; } = default!;
        public RatingDto Rating { get; set; } = default!;


        public class RatingDto
        {
            public decimal Rate { get; set; }
            public int Count { get; set; }
        }
    }

}
