using Application.Core;
using Application.DTOs.Catalogues;

namespace Application.Handlers.Catalogues.EventTable
{
    public interface IEventTableHandler
    {
        public Task<Result<List<EventTableDto>>> GetListAsync();
        public Task<Result<EventTableDto>> GetOneAsync(Guid eventId);
        public Task<Result<string>> CreateOneAsync(EventTableDto EventTableDto);
        public Task<Result<string>> EditOneAsync(EventTableDto EventTableDto);
        public Task<Result<string>> DeleteOneAsync(Guid id);
    }
}
