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

    // [HttpGet("{id}")]
    // [Authorize(Policy = "SellersOnly")]
    // public async Task<IActionResult> GetCustomersOne(Guid id)
    // {
    //     return HandleResult(await _ticketHandler.GetCustomersTicketAsync(id));
    // }

    [HttpPost]
    [Authorize(Policy = "SellersOnly")]
    public async Task<IActionResult> CreateTicket(TicketDiscountDto ticketDiscountDto)
    {
        return HandleResult(await _ticketDiscountHandler.CreateSellersOneAsync(ticketDiscountDto));
    }

    //[HttpPut("{id}")]
    //public async Task<IActionResult> EditEvent(Guid id, TicketDto ticketDto)
    //{
    //    ticketDto.Id = id;
    //    return HandleResult(await _eventHandler.EditSellersOneAsync(ticketDto));
    //}

    // [HttpDelete("{id}")]
    // [Authorize(Policy = "ModeratorsOnly")]
    // public async Task<IActionResult> DeleteEvent(Guid id)
    // {
    //     return HandleResult(await _ticketHandler.DeleteCustomersOneAsync(id));
    // }
}