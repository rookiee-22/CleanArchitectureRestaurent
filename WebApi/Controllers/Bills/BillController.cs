using Application.Features.Bills.Commands;
using Application.Features.Bills.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Bills
{
    [Route("api/bill")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BillController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBill(CreateBillCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetBills()
        {
            var result = await _mediator.Send(new GetBillsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillById(int id)
        {
            var result = await _mediator.Send(new GetBillByIdQuery(id));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBill(int id, UpdateBillCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(int id)
        {
            var result = await _mediator.Send(new DeleteBillCommand(id));
            return Ok(result);
        }
    }
}
