using Asp.Versioning;
using hr.makemystamp.com.application.Segrigations.Commands.Roles;
using hr.makemystamp.com.application.Segrigations.Queries.Roles;
using hr.makemystamp.com.core.Dtos.RoleDto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hr.makemystamp.com.api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiversion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class RoleController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize]
        [HttpPost("RoleAddition")]
        public async Task<IActionResult> RoleAddition([FromBody] RoleDto loginDto)
        {
            RoleCommand roleCommand = new(loginDto);
            var result = await _mediator.Send(roleCommand);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetAllRole")]
        public async Task<IActionResult> GetAllRoles()
        {
            GetAllRoleQuery roleCommand = new();
            var result = await _mediator.Send(roleCommand);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("GetRole/{id}")]
        public async Task<IActionResult> GetRoleById(long id)
        {
            GetRoleByIdQuery queryCommand = new GetRoleByIdQuery(id);
            var result = await _mediator.Send(queryCommand);
            return Ok(result);
        }
    }
}
