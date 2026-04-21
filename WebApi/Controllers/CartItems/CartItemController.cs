using Application.Features.CartItems.Commands;
using Application.Features.CartItems.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.CartItems
{
    [Route("api/cart-item")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCartItem(CreateCartItemCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItems()
        {
            var result = await _mediator.Send(new GetCartItemsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartItemById(int id)
        {
            var result = await _mediator.Send(new GetCartItemByIdQuery(id));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCartItem(int id, CreateCartItemCommand command)
        {
            var result = await _mediator.Send(new UpdateCartItemCommand(id, command));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var result = await _mediator.Send(new DeleteCartItemCommand(id));
            return Ok(result);
        }
    }
}
