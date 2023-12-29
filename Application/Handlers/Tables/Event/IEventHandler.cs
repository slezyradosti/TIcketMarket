using Application.Core;
using Application.DTOs.Tables;

namespace Application.Handlers.Tables.Event
{
    public interface IEventHandler
    {
        public Task<Result<List<EventDto>>> GetSellersEventListAsync();
        public Task<Result<EventDto>> GetSellersEventAsync(Guid eventId);
        public Task<Result<string>> CreateSellersOneAsync(EventDto eventDto);
        public Task<Result<string>> EditSellersOneAsync(EventDto eventDto);
        public Task<Result<string>> DeleteSellersOneAsync(Guid id);
        public Task<Result<List<EventDto>>> GetAllEventsOrderedAsync();
    }
}
