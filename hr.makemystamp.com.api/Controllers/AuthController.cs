using Asp.Versioning;
using hr.makemystamp.com.api.Attributes;
using hr.makemystamp.com.application.Segrigations.Commands.Login.Command;
using hr.makemystamp.com.application.Segrigations.Commands.UserRegister;
using hr.makemystamp.com.core.Dtos.LoginDto;
using hr.makemystamp.com.core.Dtos.RegisterUserDto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hr.makemystamp.com.api.Controllers
{

    [ApiController]
    [Route("api/v{version:apiversion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [AllowAnonymousMiddleware]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            LoginCommand loginCommand = new(loginDto);
            var result = await _mediator.Send(loginCommand);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            RegisterUserCommand registerCommand = new(registerUserDto);
            var result = await _mediator.Send(registerCommand);
            return Ok(result);
        }
    }
}
