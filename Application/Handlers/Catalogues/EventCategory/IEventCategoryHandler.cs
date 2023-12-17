using Application.Core;
using Application.DTOs.Catalogues;

namespace Application.Handlers.Catalogues.EventCategory
{
    public interface IEventCategoryHandler
    {
        public Task<Result<List<EventCategoryDto>>> GetListAsync();
        public Task<Result<EventCategoryDto>> GetOneAsync(Guid eventId);
        public Task<Result<string>> CreateOneAsync(EventCategoryDto eventCategoryDto);
        public Task<Result<string>> EditOneAsync(EventCategoryDto eventCategoryDto);
        public Task<Result<string>> DeleteOneAsync(Guid id);
    }
}
