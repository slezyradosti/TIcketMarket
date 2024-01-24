using Application.Core;
using Application.DTOs.Tables;
using Domain.Repositories.DTOs;

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
        public Task<Result<EventDto>> GetAnyEventAsync(Guid eventId);
        public Task<Result<List<EventExtendedDto>>> GetAllEventsExtendedOrderedAsync();
    }
}
