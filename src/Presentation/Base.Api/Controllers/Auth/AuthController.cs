using Base.Api.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Base.Application.Features.Auth.Commands;
using Base.Application.Features.Auth.Requests;

namespace Base.Api.Controllers.Auth
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var command = _mapper.Map<RegisterUserCommand>(request);
            var result = await _mediator.Send(command);

            return Ok(result);
            //return result
            //    ? Ok(ApiResponse<object>.SuccessResponse(null, "Usuário criado com sucesso."))
            //    : BadRequest(ApiResponse<object>.FailResponse("Erro ao criar usuário."));
        }
    }
}
