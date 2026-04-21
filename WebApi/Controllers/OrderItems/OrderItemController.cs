using Application.Features.OrderItems.Commands;
using Application.Features.OrderItems.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.OrderItems
{
    [Route("api/order-item")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItem(CreateOrderItemCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderItems()
        {
            var result = await _mediator.Send(new GetOrderItemsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItemById(int id)
        {
            var result = await _mediator.Send(new GetOrderItemByIdQuery(id));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, CreateOrderItemCommand command)
        {
            var result = await _mediator.Send(new UpdateOrderItemCommand(id, command));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var result = await _mediator.Send(new DeleteOrderItemCommand(id));
            return Ok(result);
        }
    }
}
