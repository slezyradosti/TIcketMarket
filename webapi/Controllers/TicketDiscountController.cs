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
    [Route("my-discounts")]
    [Authorize(Policy = "SellersOnly")]
    public async Task<IActionResult> GetSellersTicketList()//[FromQuery] RequestDto request)
    {
        return HandleResult(await _ticketDiscountHandler.GetSellersTicketDiscountListAsync());
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "SellersOnly")]
    public async Task<IActionResult> GetSellersOne(Guid id)
    {
        return HandleResult(await _ticketDiscountHandler.GetSellersDiscountAsync(id));
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
        return HandleResult(await _ticketDiscountHandler.EditSellersOneAsync(ticketDiscountDto));
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "SellersOnly")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        return HandleResult(await _ticketDiscountHandler.DeleteSellersOneAsync(id));
    }
}