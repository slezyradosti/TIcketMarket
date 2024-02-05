using Application.Core;
using Application.DTOs.Tables;

namespace Application.Handlers.Tables.TableEvent
{
    public interface ITableEventHandler
    {
        public Task<Result<List<TableEventDto>>> GetEventsTabletListAsync(Guid eventId);
        public Task<Result<TableEventDto>> GetEventsTableDetailedAsync(Guid id);
        public Task<Result<string>> CreateEventsTableAsync(TableEventDto tableEventDto);
        public Task<Result<string>> EditEventsTableAsync(TableEventDto tableEventDto);
        public Task<Result<string>> DeleteEventsTableAsync(Guid tableEventId);
    }
}
