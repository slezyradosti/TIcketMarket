using Application.Core;
using Application.DTOs.Tables;
using Application.DTOs.Users.HTTP;
using AutoMapper;
using Domain.Repositories.DTOs;
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

        public async Task<Result<List<EventDto>>> GetSellersEventListAsync()
        {
            var userId = _userAccessor.GetUserId();

            var events = await _eventRepository.GetSellersEventListSortedAsync(userId);

            List<EventDto> eventDtos = new List<EventDto>();

            _mapper.Map(events, eventDtos);

            return Result<List<EventDto>>.Success(eventDtos);
        }

        public async Task<Result<EventDto>> GetSellersEventAsync(Guid eventId)
        {

            if (!await _eventRepository.HasUserAccessToTheEventAsync(eventId,
                    _userAccessor.GetUserId()))
            {
                return Result<EventDto>.Failure("You have no access to this data");
            }

            var evnt = await _eventRepository.GetOneDetailedAsync(eventId);

            EventDto eventDto = new EventDto();

            _mapper.Map(evnt, eventDto);

            return Result<EventDto>.Success(eventDto);
        }
        
        public async Task<Result<EventDto>> GetAnyEventAsync(Guid eventId)
        {
            var evnt = await _eventRepository.GetOneDetailedAsync(eventId);

            EventDto eventDto = new EventDto();

            _mapper.Map(evnt, eventDto);

            return Result<EventDto>.Success(eventDto);
        }

        public async Task<Result<string>> CreateSellersOneAsync(EventDto eventDto)
        {
            eventDto.UserId = _userAccessor.GetUserId();
            
            // free places = total spaces by default;
            //eventDto.FreePlaces ??= eventDto.TotalPlaces;
            
            var evnt = new Domain.Models.Tables.Event();
            _mapper.Map(eventDto, evnt);

            var result = await _eventRepository.AddAsync(evnt) > 0;

            if (!result) return Result<string>.Failure("Failed to create Event");

            return Result<string>.Success("Successfully");
        }

        public async Task<Result<string>> EditSellersOneAsync(EventDto eventDto)
        {
            var evnt = await _eventRepository.GetOneAsync(eventDto.Id);

            if (evnt == null) return null;

            var userId = _userAccessor.GetUserId();

            if (!await _eventRepository.HasUserAccessToTheEventAsync(eventDto.Id.Value, userId))
            {
                return Result<string>.Failure("You have no access to this data");
            }

            eventDto.UserId ??= userId;
            _mapper.Map(eventDto, evnt);

            var result = await _eventRepository.SaveAsync(evnt) > 0;

            if (!result) return Result<string>.Failure("Failed to update Event");

            return Result<string>.Success("Successfully");
        }

        public async Task<Result<string>> DeleteSellersOneAsync(Guid eventId)
        {
            var evnt = await _eventRepository.GetOneAsync(eventId);

            if (evnt == null) return null;

            if (!await _eventRepository.HasUserAccessToTheEventAsync(eventId, _userAccessor.GetUserId()))
            {
                return Result<string>.Failure("You have no access to this data");
            }

            var result = await _eventRepository.RemoveAsync(evnt) > 0;

            if (!result) return Result<string>.Failure("Failed to delete Event");

            return Result<string>.Success("Successfully");
        }
        
        public async Task<Result<List<EventDto>>> GetAllEventsOrderedAsync()
        {
            var events = await _eventRepository.GetAllEventsOrderedAsync();

            List<EventDto> eventDtos = new List<EventDto>();

            _mapper.Map(events, eventDtos);

            return Result<List<EventDto>>.Success(eventDtos);
        }
        
        public async Task<Result<List<EventExtendedDto>>> GetAllEventsExtendedOrderedAsync()
        {
            var events = await _eventRepository.GetAllExtendedEventsOrderedAsync();

            List<EventExtendedDto> eventDtos = new List<EventExtendedDto>();

            _mapper.Map(events, eventDtos);

            return Result<List<EventExtendedDto>>.Success(eventDtos);
        }
        
        public async Task<Result<EventExtendedDto>> GetAnyEventExtendedAsync(Guid eventId)
        {
            var evnt = await _eventRepository.GetOneExtendedAsync(eventId);

            EventExtendedDto eventDto = new EventExtendedDto();

            _mapper.Map(evnt, eventDto);

            return Result<EventExtendedDto>.Success(eventDto);
        }
    }
}
