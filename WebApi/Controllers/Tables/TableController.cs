using Application.Features.Tables.Commands;
using Application.Features.Tables.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Tables
{
    [Route("api/table")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TableController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTable(CreateTableCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTables()
        {
            var result = await _mediator.Send(new GetTablesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTableById(int id)
        {
            var result = await _mediator.Send(new GetTableByIdQuery(id));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTable(int id, CreateTableCommand command)
        {
            var result = await _mediator.Send(new UpdateTableCommand(id, command));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _mediator.Send(new DeleteTableCommand(id));
            return Ok(result);
        }
    }
}
