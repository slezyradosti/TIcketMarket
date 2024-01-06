using Application.DTOs.Catalogues;
using Application.Handlers.Catalogues.EventTable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Authorize]
    public class EventTableController : BaseApiController
    {
        private readonly IEventTableHandler _eventTableHandler;

        public EventTableController(IEventTableHandler eventTableHandler)
        {
            _eventTableHandler = eventTableHandler;
        }

        [HttpGet("list")]
        [Authorize(Policy = "SellersOnly")]
        public async Task<IActionResult> GetList()
        {
            return HandleResult(await _eventTableHandler.GetListAsync());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "SellersOnly")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            return HandleResult(await _eventTableHandler.GetOneAsync(id));
        }

        [HttpPost]
        [Authorize(Policy = "SellersOnly")]
        public async Task<IActionResult> CreateOne(EventTableDto eventTableDto)
        {
            return HandleResult(await _eventTableHandler.CreateOneAsync(eventTableDto));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "ModeratorsOnly")] // ??? can seller edit??
        public async Task<IActionResult> EditOne(Guid id, EventTableDto eventTableDto)
        {
            eventTableDto.Id = id;
            return HandleResult(await _eventTableHandler.EditOneAsync(eventTableDto));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "ModeratorsOnly")]
        public async Task<IActionResult> DeleteOne(Guid id)
        {
            return HandleResult(await _eventTableHandler.DeleteOneAsync(id));
        }
    }
}
