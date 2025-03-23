using Base.Api.Common;
using Base.Application.Features.Products.Commands;
using Base.Application.Features.Products.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Base.Api.Controllers.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
            => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand cmd) => Ok(await _mediator.Send(cmd));
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetProductsQuery query) => Ok(await _mediator.Send(query));

        [HttpGet("{id:int}")]   
        public async Task<IActionResult> GetById(int id) => Ok(await _mediator.Send(new GetProductByIdQuery(id)));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand command)
        {
            command.Id = id;    
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteProductCommand { Id = id });
            return Ok(new { message = "Produto removido com sucesso." });
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category, [FromQuery] GetProductsByCategoryQuery query)
        {
            query.Category = category;
            return Ok(await _mediator.Send(query));
        }

    }
}
