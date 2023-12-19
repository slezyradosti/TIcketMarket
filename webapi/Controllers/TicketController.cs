using Application.DTOs.Tables;
using Application.Handlers.Tables.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Authorize]
    public class TicketController : BaseApiController
    {
        private readonly ITicketHandler _ticketHandler;

        public TicketController(ITicketHandler ticketHandler)
        {
            _ticketHandler = ticketHandler;
        }

        [HttpGet]
        [Route("MyTickets")]
        [Authorize(Policy = "CustomersOnly")]
        public async Task<IActionResult> GetCustomersTicketList()//[FromQuery] RequestDto request)
        {
            return HandleResult(await _ticketHandler.GetCustomersTicketListAsync());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "CustomersOnly")]
        public async Task<IActionResult> GetCustomersOne(Guid id)
        {
            return HandleResult(await _ticketHandler.GetCustomersTicketAsync(id));
        }

        [HttpPost]
        //[Authorize(Policy = "SellersOnly")] ???
        public async Task<IActionResult> CreateTicket(TicketDto ticketDto)
        {
            return HandleResult(await _ticketHandler.CreateCustomersOneAsync(ticketDto));
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> EditEvent(Guid id, TicketDto ticketDto)
        //{
        //    ticketDto.Id = id;
        //    return HandleResult(await _eventHandler.EditSellersOneAsync(ticketDto));
        //}

        [HttpDelete("{id}")]
        [Authorize(Policy = "ModeratorsOnly")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            return HandleResult(await _ticketHandler.DeleteCustomersOneAsync(id));
        }
    }
}
