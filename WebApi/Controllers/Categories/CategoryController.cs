using Application.Features.Categories.Commands;
using Application.Features.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Categories
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _mediator.Send(new GetCategoryQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(id));
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id,CreateCategoryCommand command)
        {
            var result = await _mediator.Send(new UpdateCategoryCommand(id, command));
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id) 
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(id));
            return Ok(result);
        }
    }
}
