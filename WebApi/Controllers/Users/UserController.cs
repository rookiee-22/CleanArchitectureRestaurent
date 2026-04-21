using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Users
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles ="1")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _mediator.Send(new GetUsersQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id) 
        {
            var result= await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, CreateUserCommand command)
        {
            var result = await _mediator.Send(new UpdateUserCommand(id, command));
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id));
            return Ok(result);
        }
        

    }
}
