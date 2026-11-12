using KodaStore.Api.DTOs;
using KodaStore.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace KodaStore.Api.Controllers;

[ApiController]
[Route("api/v1/orders")]

public sealed class OrderController(IOrderService service) : ControllerBase
{
    private readonly IOrderService _service = service;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderSummaryResponse>>> GetPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        try
        {
            return Ok(await _service.GetPageAsync(page,pageSize,cancellationToken)); 
        }
        catch (ArgumentException e)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Invalid Pagination",
                Detail=e.Message,
                Status =400 
            });
        }
    }


    [HttpGet("{id:long}")]
    public async Task<ActionResult<OrderDetailResponse>> GetById(
        [FromRoute] long id,CancellationToken cancellationToken
    )
    {
        var order = await _service.GetByIdAsync(id,cancellationToken);

        return order is null ? NotFound() : Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDetailResponse>> Create(
        [FromBody] CreateOrderRequest request,CancellationToken cancellationToken
    )
    {
        try
        {
            var created = await _service.CreateAsync(request,cancellationToken);

            return CreatedAtAction(nameof(GetById), new {id = created.OrderId}, created);
        }
        catch (ArgumentException e)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Invalid Pagination",
                Detail=e.Message,
                Status =400 
            });
        }
    }


    [HttpPatch("{id:long}/confirm")]
    public async Task<ActionResult<OrderDetailResponse>> Confirm(
        [FromRoute] long id, CancellationToken cancellationToken
    )   
    {

        try
        {
            var result = await _service.ConfirmAsync(id,cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }
        catch (InvalidOperationException e)
        {
            return Conflict(new ProblemDetails
            {
                Title = "Invalid Order",
                Detail=e.Message,
                Status =409 
            });
        }
    }

}