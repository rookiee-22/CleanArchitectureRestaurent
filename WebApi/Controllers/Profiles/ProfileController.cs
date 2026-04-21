using Application.Features.Profiles.Commands;
using Application.Features.Profiles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Profiles
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile(CreateProfileCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProfiles()
        {
            var result = await _mediator.Send(new GetProfilesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileById(int id)
        {
            var result = await _mediator.Send(new GetProfileByIdQuery(id));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(int id, CreateProfileCommand command)
        {
            var result = await _mediator.Send(new UpdateProfileCommand(id, command));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var result = await _mediator.Send(new DeleteProfileCommand(id));
            return Ok(result);
        }
    }
}
