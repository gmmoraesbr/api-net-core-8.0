using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.Features.Orders.Requests
{
    public class CreateOrderRequest
    {
        public required List<CreateOrderItemRequest> Items { get; set; }
    }
}
