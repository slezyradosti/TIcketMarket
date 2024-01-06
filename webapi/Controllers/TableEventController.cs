
using Application.DTOs.Tables;
using Application.Handlers.Tables.TableEvent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Authorize(Policy = "SellersOnly")]
    public class TableEventController : BaseApiController
    {
        private readonly ITableEventHandler _tableEventHandler;

        public TableEventController(ITableEventHandler tableEventHandler)
        {
            _tableEventHandler = tableEventHandler;
        }

        [HttpGet("List/{eventId}")]
        public async Task<IActionResult> GetEventsTabletList(Guid eventId)//[FromQuery] RequestDto request)
        {
            return HandleResult(await _tableEventHandler.GetEventsTabletListAsync(eventId));
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEventsTableDetailed(Guid eventId)
        {
            return HandleResult(await _tableEventHandler.GetEventsTableDetailedAsync(eventId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEventsTable(TableEventDto tableEventDto)
        {
            return HandleResult(await _tableEventHandler.CreateEventsTableAsync(tableEventDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditEventsTable(Guid id, TableEventDto tableEventDto)
        {
            tableEventDto.Id = id;
            return HandleResult(await _tableEventHandler.EditEventsTableAsync(tableEventDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventsTable(Guid id)
        {
            return HandleResult(await _tableEventHandler.DeleteEventsTableAsync(id));
        }
    }
}
