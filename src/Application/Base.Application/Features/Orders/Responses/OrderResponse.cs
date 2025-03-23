using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.Features.Orders.Responses
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }
        public decimal Total { get; set; }
        public required List<OrderItemResponse> Items { get; set; }
    }
}
