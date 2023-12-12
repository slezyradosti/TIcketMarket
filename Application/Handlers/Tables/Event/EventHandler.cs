using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain.Repositories.Repos.Interfaces.Tables;

namespace Application.Handlers.Tables.Event
{
    public class EventHandler : IEventHandler
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<EventDto>>> GetListAsync()
        {
            var events = await _eventRepository.GetAllAsync();

            List<EventDto> eventDtos = new List<EventDto>();

            _mapper.Map(events, eventDtos);

            return Result<List<EventDto>>.Success(eventDtos);
        }
    }
}
