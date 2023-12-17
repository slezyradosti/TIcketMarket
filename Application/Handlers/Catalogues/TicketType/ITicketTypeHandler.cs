using Application.Core;
using Application.DTOs.Catalogues;

namespace Application.Handlers.Catalogues.TicketType
{
    public interface ITicketTypeHandler
    {
        public Task<Result<List<TicketTypeDto>>> GetListAsync();
        public Task<Result<TicketTypeDto>> GetOneAsync(Guid eventId);
        public Task<Result<string>> CreateOneAsync(TicketTypeDto TicketTypeDto);
        public Task<Result<string>> EditOneAsync(TicketTypeDto TicketTypeDto);
        public Task<Result<string>> DeleteOneAsync(Guid id);
    }
}
