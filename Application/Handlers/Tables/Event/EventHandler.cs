using Application.Core;
using Application.DTOs;
using Application.DTOs.Users.HTTP;
using AutoMapper;
using Domain.Repositories.Repos.Interfaces.Tables;

namespace Application.Handlers.Tables.Event
{
    public class EventHandler : IEventHandler
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public EventHandler(IEventRepository eventRepository, IMapper mapper, IUserAccessor userAccessor)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _userAccessor = userAccessor;
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
