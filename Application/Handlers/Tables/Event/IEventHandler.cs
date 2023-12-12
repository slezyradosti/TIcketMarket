using Application.Core;
using Application.DTOs;

namespace Application.Handlers.Tables.Event
{
    public interface IEventHandler
    {
        public Task<Result<List<EventDto>>> GetListAsync();
    }
}
