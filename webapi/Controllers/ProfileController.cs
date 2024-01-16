using Application.Handlers.Tables.Order;
using Application.Handlers.Tables.TicketOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[Authorize]
public class ProfileController : BaseApiController
{
    private readonly IOrderHandler _orderHandler;
    private readonly ITicketOrderHandler _ticketOrderHandler;

    public ProfileController(IOrderHandler orderHandler, ITicketOrderHandler ticketOrderHandler)
    {
        _orderHandler = orderHandler;
        _ticketOrderHandler = ticketOrderHandler;
    }
    
    [HttpGet("my-orders")]
    [Authorize(Policy = "CustomersOnly")]
    public async Task<IActionResult> GetCustomersOrderList()
    {
        return HandleResult(await _orderHandler.GetCustomersOrderListAsync());
    }
    
    [HttpGet("my-tickets")]
    [Authorize(Policy = "CustomersOnly")]
    public async Task<IActionResult> GetCustomersTicketList()
    {
        return HandleResult(await _ticketOrderHandler.GetCustomersTicketListAsync());
    }
}