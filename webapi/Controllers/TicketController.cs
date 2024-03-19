using Application.DTOs.Requests;
using Application.DTOs.Tables;
using Application.Handlers.Tables.Ticket;
using Application.Handlers.Tables.TicketOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Authorize]
    public class TicketController : BaseApiController
    {
        private readonly ITicketHandler _ticketHandler;
        private readonly ITicketOrderHandler _ticketOrderHandler;

        public TicketController(ITicketHandler ticketHandler, ITicketOrderHandler ticketOrderHandler)
        {
            _ticketHandler = ticketHandler;
            _ticketOrderHandler = ticketOrderHandler;
        }

        // [HttpGet("my-tickets")]
        // [Authorize(Policy = "CustomersOnly")]
        // public async Task<IActionResult> GetCustomersTicketList()//[FromQuery] RequestDto request)
        // {
        //     return HandleResult(await _ticketOrderHandler.GetCustomersTicketListAsync());
        // }

        [HttpGet("available-tickets/{eventId}")]
        [Authorize(Policy = "SellersOnly ")]
        public async Task<IActionResult> GetAvailableTicketList(Guid eventId)
        {
            return HandleResult(await _ticketHandler.GetAvailableTicketListAsync(eventId));
        }

        [HttpGet("events-tickets/{eventId}")]
        [Authorize(Policy = "SellersOnly ")]
        public async Task<IActionResult> GetDetailedTicketList(Guid eventId)
        {
            return HandleResult(await _ticketHandler.GetSellersDetailedTicketListAsync(eventId));
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "CustomersOnly")]
        public async Task<IActionResult> GetCustomersOne(Guid id)
        {
            return HandleResult(await _ticketHandler.GetCustomersTicketAsync(id));
        }

        [HttpPost]
        [Authorize(Policy = "SellersOnly")] // ???
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
        [Authorize(Policy = "SellersOnly")]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            return HandleResult(await _ticketHandler.DeleteCustomersOneAsync(id));
        }

        [HttpPost("generate-tickets")]
        [Authorize(Policy = "SellersOnly")]
        public async Task<IActionResult> GenerateTickets([FromBody] TicketDto ticketDto,
            [FromQuery(Name = "ticket-amount")] int ticketAmount)
        {
            return HandleResult(await _ticketHandler.GenerateEventsTicketList(ticketDto, ticketAmount));
        }

        [HttpGet("event-ticket-amount/{eventId}")]
        //[Authorize(Policy = "CustomersOnly")] // ?? anonymous
        [AllowAnonymous]
        public async Task<IActionResult> GetEventTicketAmount(Guid eventId)
        {
            return HandleResult(await _ticketHandler.GetEventTicketsAmountAsync(eventId));
        }

        [HttpPut("apply-discount")]
        //[Authorize(Policy = "CustomersOnly")]
        [AllowAnonymous]
        public async Task<IActionResult> ApplyDiscount(ApplyDiscountDto applyDiscountDto)
        {
            return HandleResult(await _ticketHandler.ApplyDiscountTransactionAsync(applyDiscountDto));
        }

        [HttpPut("remove-discount/{ticketId}")]
        //[Authorize(Policy = "CustomersOnly")] // ?? 
        [AllowAnonymous]
        public async Task<IActionResult> RemoveDiscount(Guid ticketId)
        {
            return HandleResult(await _ticketHandler.RemoveDiscountTransactionAsync(ticketId));
        }

        [HttpGet("ticket-to-buy")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTicketToBuy([FromQuery(Name = "event-id")] Guid eventId,
            [FromQuery(Name = "ticket-type-id")] Guid ticketTypeId)
        {
            return HandleResult(await _ticketHandler.GetTicketToBuyAsync(eventId, ticketTypeId));
        }

        [HttpPost("purchase-ticket/{ticketId}")]
        [Authorize(Policy = "CustomersOnly")]
        public async Task<IActionResult> PurchaseTicket(Guid ticketId)
        {
            return HandleResult(await _ticketHandler.PurchaseTicket(ticketId));
        }
    }
}
