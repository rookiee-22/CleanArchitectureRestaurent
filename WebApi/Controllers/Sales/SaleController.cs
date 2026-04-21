using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Sales;

[Route("api/sale")]
[ApiController]
public class SaleController : ControllerBase
{
    private readonly IMediator _mediator;

    public SaleController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> GetSales()
    {
        var result = await _mediator.Send(new Application.Features.Sales.GetSalesQuery());
        return Ok(result);
    }

}
