using Application.DTOs.Catalogues;
using Application.Handlers.Catalogues.TicketDiscount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[Authorize]
public class TicketDiscountController : BaseApiController
{
    private readonly ITicketDiscountHandler _ticketDiscountHandler;

    public TicketDiscountController(ITicketDiscountHandler ticketDiscountHandler)
    {
        _ticketDiscountHandler = ticketDiscountHandler;
    }
    
    [HttpGet]
    [Route("MyDiscounts")]
    [Authorize(Policy = "SellersOnly")]
    public async Task<IActionResult> GetCustomersTicketList()//[FromQuery] RequestDto request)
    {
        return HandleResult(await _ticketDiscountHandler.GetCustomersTicketDiscountListAsync());
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "SellersOnly")]
    public async Task<IActionResult> GetSellersOne(Guid id)
    {
        return HandleResult(await _ticketDiscountHandler.GetCustomersDiscountAsync(id));
    }

    [HttpPost]
    [Authorize(Policy = "SellersOnly")]
    public async Task<IActionResult> CreateTicket(TicketDiscountDto ticketDiscountDto)
    {
        return HandleResult(await _ticketDiscountHandler.CreateSellersOneAsync(ticketDiscountDto));
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "SellersOnly")]
    public async Task<IActionResult> EditEvent(Guid id, TicketDiscountDto ticketDiscountDto)
    {
        ticketDiscountDto.Id = id;
        return HandleResult(await _ticketDiscountHandler.EditCustomersOneAsync(ticketDiscountDto));
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "SellersOnly")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        return HandleResult(await _ticketDiscountHandler.DeleteCustomersOneAsync(id));
    }
}