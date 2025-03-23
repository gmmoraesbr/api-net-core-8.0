//using Base.Api.Common;
//using Base.Application.Features.Orders.Commands;
//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace Base.Api.Controllers.Orders;

//[Authorize]
//[ApiController]
//[Route("Api/[controller]")]
//public class OrdersController : ControllerBase
//{
//    private readonly IMediator _mediator;

//    public OrdersController(IMediator mediator)
//    {
//        _mediator = mediator;
//    }

//    [HttpPost]
//    public async Task<IActionResult> Create(CreateOrderCommand command)
//    {
//        var id = await _mediator.Send(command);
//        CreatedAtAction(nameof(GetById), new { id }, new { id });

//        var response = new ApiResponse<object>
//        {
//            Data = new { OrderId = id },
//            Message = "Pedido criado com sucesso!",
//            Success = true
//        };

//        return Ok(response);
//    }

//    [HttpGet("{id}")]
//    public IActionResult GetById(Guid id)
//    {
//        return Ok();
//    }
//}
