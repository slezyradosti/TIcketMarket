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

        [HttpGet("MyEvents")]
        [Authorize(Policy = "SellersOnly")]
        public async Task<IActionResult> GetSellersEventList()//[FromQuery] RequestDto request)
        {
            return HandleResult(await _eventHandler.GetSellersEventListAsync());
        }
        
        [HttpGet("List")]
        [Authorize(Policy = "CustomersOnly")]
        public async Task<IActionResult> GetEventList()
        {
            return HandleResult(await _eventHandler.GetAllEventsOrderedAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSellersOne(Guid id)
        {
            return HandleResult(await _eventHandler.GetSellersEventAsync(id));
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
