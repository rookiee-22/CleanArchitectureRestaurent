using Application.Features.BillItems.Commands;
using Application.Features.BillItems.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.BillItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BillItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBillItem(CreateBillItemCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetBillItems()
        {
            var result = await _mediator.Send(new GetBillItemsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillItemById(int id)
        {
            var result = await _mediator.Send(new GetBillItemByIdQuery(id));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBillItem(int id, CreateBillItemCommand command)
        {
            var result = await _mediator.Send(new UpdateBillItemCommand(id, command));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBillItem(int id)
        {
            var result = await _mediator.Send(new DeleteBillItemCommand(id));
            return Ok(result);
        }
    }
}
