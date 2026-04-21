using Application.Features.Products.Commands;
using Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Products
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _mediator.Send(new GetProductQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, CreateProductCommand command)
        {
            var result = await _mediator.Send(new UpdateProductCommand(id, command));
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result= await _mediator.Send(new  DeleteProductCommand(id));
            return Ok(result);
        }
    }
}
