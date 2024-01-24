using Application.DTOs.Tables;
using Application.Handlers.Tables.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Authorize(Policy = "SellersOnly")]
    public class EventController : BaseApiController
    {
        private readonly IEventHandler _eventHandler;

        public EventController(IEventHandler eventHandler)
        {
            _eventHandler = eventHandler;
        }

        [HttpGet("my-events")]
        public async Task<IActionResult> GetSellersEventList()//[FromQuery] RequestDto request)
        {
            return HandleResult(await _eventHandler.GetSellersEventListAsync());
        }

        [HttpGet("List")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventList()
        {
            return HandleResult(await _eventHandler.GetAllEventsExtendedOrderedAsync());
        }

        [HttpGet("my-event/{id}")]
        public async Task<IActionResult> GetSellersOne(Guid id)
        {
            return HandleResult(await _eventHandler.GetSellersEventAsync(id));
        }
        
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOne(Guid id)
        {
            return HandleResult(await _eventHandler.GetAnyEventAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventDto eventDto)
        {
            return HandleResult(await _eventHandler.CreateSellersOneAsync(eventDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditEvent(Guid id, EventDto eventDto)
        {
            eventDto.Id = id;
            return HandleResult(await _eventHandler.EditSellersOneAsync(eventDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            return HandleResult(await _eventHandler.DeleteSellersOneAsync(id));
        }
    }
}
