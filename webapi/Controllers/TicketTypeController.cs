using Application.DTOs.Catalogues;
using Application.Handlers.Catalogues.TicketType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class TicketTypeController : BaseApiController
    {
        private readonly ITicketTypeHandler _ticketTypeHandler;

        public TicketTypeController(ITicketTypeHandler ticketTypeHandler)
        {
            _ticketTypeHandler = ticketTypeHandler;
        }

        [HttpGet("list")]
        [Authorize(Policy = "SellersOnly")]
        public async Task<IActionResult> GetList()
        {
            return HandleResult(await _ticketTypeHandler.GetListAsync());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "SellersOnly")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            return HandleResult(await _ticketTypeHandler.GetOneAsync(id));
        }

        [HttpPost]
        [Authorize(Policy = "SellersOnly")]
        public async Task<IActionResult> CreateOne(TicketTypeDto ticketTypeDto)
        {
            return HandleResult(await _ticketTypeHandler.CreateOneAsync(ticketTypeDto));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "ModeratorsOnly")] // ??? can seller edit??
        public async Task<IActionResult> EditOne(Guid id, TicketTypeDto ticketTypeDto)
        {
            ticketTypeDto.Id = id;
            return HandleResult(await _ticketTypeHandler.EditOneAsync(ticketTypeDto));
        }

        [HttpDelete("{id}")]
        // TODO if sellets can delete TicketType?
        [Authorize(Policy = "SellersOnly")]
        public async Task<IActionResult> DeleteOne(Guid id)
        {
            return HandleResult(await _ticketTypeHandler.DeleteOneAsync(id));
        }
    }
}
