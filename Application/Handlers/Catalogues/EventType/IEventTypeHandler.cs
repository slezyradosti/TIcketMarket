using Application.Core;
using Application.DTOs.Catalogues;

namespace Application.Handlers.Catalogues.EventType
{
    public interface IEventTypeHandler
    {
        public Task<Result<List<EventTypeDto>>> GetListAsync();
        public Task<Result<EventTypeDto>> GetOneAsync(Guid eventId);
        public Task<Result<string>> CreateOneAsync(EventTypeDto eventTypeDto);
        public Task<Result<string>> EditOneAsync(EventTypeDto eventTypeDto);
        public Task<Result<string>> DeleteOneAsync(Guid id);
    }
}
