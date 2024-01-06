using Application.DTOs.Catalogues;
using Application.Handlers.Catalogues.EventCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Authorize]
    public class EventCategoryController : BaseApiController
    {
        private readonly IEventCategoryHandler _eventCategoryHandler;

        public EventCategoryController(IEventCategoryHandler eventCategoryHandler)
        {
            _eventCategoryHandler = eventCategoryHandler;
        }

        [HttpGet("List")]
        [Authorize(Policy = "SellersOnly")]
        public async Task<IActionResult> GetList()
        {
            return HandleResult(await _eventCategoryHandler.GetListAsync());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "SellersOnly")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            return HandleResult(await _eventCategoryHandler.GetOneAsync(id));
        }

        [HttpPost]
        [Authorize(Policy = "SellersOnly")]
        public async Task<IActionResult> CreateEvent(EventCategoryDto eventCategoryDto)
        {
            return HandleResult(await _eventCategoryHandler.CreateOneAsync(eventCategoryDto));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "ModeratorsOnly")] // ??? can seller edit??
        public async Task<IActionResult> EditEvent(Guid id, EventCategoryDto eventCategoryDto)
        {
            eventCategoryDto.Id = id;
            return HandleResult(await _eventCategoryHandler.EditOneAsync(eventCategoryDto));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "ModeratorsOnly")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            return HandleResult(await _eventCategoryHandler.DeleteOneAsync(id));
        }
    }
}
