using Application.DTOs.Tables;
using Application.Handlers.Tables.TicketOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Authorize]
    public class TicketOrderController : BaseApiController
    {
        private readonly ITicketOrderHandler _ticketOrderHandler;

        public TicketOrderController(ITicketOrderHandler ticketOrderHandler)
        {
            _ticketOrderHandler = ticketOrderHandler;
        }

        [HttpGet]
        [Route("my-ticket-orders")]
        [Authorize(Policy = "CustomersOnly")]
        public async Task<IActionResult> GetCustomersTicketList()//[FromQuery] RequestDto request)
        {
            return HandleResult(await _ticketOrderHandler.GetCustomersTicketListAsync());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "CustomersOnly")]
        public async Task<IActionResult> GetCustomersOneDetailed(Guid id)
        {
            return HandleResult(await _ticketOrderHandler.GetCustomersTicketOrderDetailedAsync(id));
        }

        [HttpPost]
        //[Authorize(Policy = "SellersOnly")] ???
        // TODO
        [Authorize(Policy = "ModeratorsOnly")] // for now for protection
        public async Task<IActionResult> CreateTicketOrder(TicketOrderDto ticketOrderDto)
        {
            return HandleResult(await _ticketOrderHandler.CreateCustomersOneAsync(ticketOrderDto));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "ModeratorsOnly")]
        public async Task<IActionResult> EditEvent(Guid id, TicketOrderDto ticketOrderDto)
        {
            ticketOrderDto.Id = id;
            return HandleResult(await _ticketOrderHandler.EditCustomersOneAsync(ticketOrderDto));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "ModeratorsOnly")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            return HandleResult(await _ticketOrderHandler.DeleteCustomersOneAsync(id));
        }
    }
}
