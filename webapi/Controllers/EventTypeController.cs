using Application.DTOs.Catalogues;
using Application.Handlers.Catalogues.EventType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Authorize]
    public class EventTypeController : BaseApiController
    {
        private readonly IEventTypeHandler _eventTypeHandler;

        public EventTypeController(IEventTypeHandler eventTypeHandler)
        {
            _eventTypeHandler = eventTypeHandler;
        }

        [HttpGet]
        [Authorize(Policy = "SellersOnly")]
        public async Task<IActionResult> GetList()
        {
            return HandleResult(await _eventTypeHandler.GetListAsync());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "SellersOnly")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            return HandleResult(await _eventTypeHandler.GetOneAsync(id));
        }

        [HttpPost]
        [Authorize(Policy = "SellersOnly")]
        public async Task<IActionResult> CreateOne(EventTypeDto eventTypeDto)
        {
            return HandleResult(await _eventTypeHandler.CreateOneAsync(eventTypeDto));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "ModeratorsOnly")] // ??? can seller edit??
        public async Task<IActionResult> EditOne(Guid id, EventTypeDto eventTypeDto)
        {
            eventTypeDto.Id = id;
            return HandleResult(await _eventTypeHandler.EditOneAsync(eventTypeDto));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "ModeratorsOnly")]
        public async Task<IActionResult> DeleteOne(Guid id)
        {
            return HandleResult(await _eventTypeHandler.DeleteOneAsync(id));
        }
    }
}
