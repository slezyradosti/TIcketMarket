using Application.Core;
using Application.DTOs.Catalogues;
using AutoMapper;
using Domain.Repositories.Repos.Interfaces.Catalogues;

namespace Application.Handlers.Catalogues.EventType
{
    public class EventTypeHandler : IEventTypeHandler
    {
        private readonly IEventTypeRepository _eventTypeRepository;
        private readonly IMapper _mapper;

        public EventTypeHandler(IEventTypeRepository eventTypeRepository, IMapper mapper)
        {
            _eventTypeRepository = eventTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<EventTypeDto>>> GetListAsync()
        {
            var eventTypes = await _eventTypeRepository.GetAllAsync();

            List<EventTypeDto> eventTypeDtos = new List<EventTypeDto>();

            _mapper.Map(eventTypes, eventTypeDtos);

            return Result<List<EventTypeDto>>.Success(eventTypeDtos);
        }

        public async Task<Result<EventTypeDto>> GetOneAsync(Guid eventId)
        {
            var eventType = await _eventTypeRepository.GetOneAsync(eventId);

            EventTypeDto eventTypeDtos = new EventTypeDto();

            _mapper.Map(eventType, eventTypeDtos);

            return Result<EventTypeDto>.Success(eventTypeDtos);
        }

        public async Task<Result<string>> CreateOneAsync(EventTypeDto eventTypeDto)
        {
            var eventType= new Domain.Models.Catalogues.EventType();
            _mapper.Map(eventTypeDto, eventType);

            var result = await _eventTypeRepository.AddAsync(eventType) > 0;

            if (!result) return Result<string>.Failure("Failed to create Event Category");

            return Result<string>.Success("Successfully");
        }

        public async Task<Result<string>> EditOneAsync(EventTypeDto eventTypeDto)
        {
            var eventType = await _eventTypeRepository.GetOneAsync(eventTypeDto.Id);

            if (eventType == null) return null;

            _mapper.Map(eventTypeDto, eventType);

            var result = await _eventTypeRepository.SaveAsync(eventType) > 0;

            if (!result) return Result<string>.Failure("Faild to edit unit");

            return Result<string>.Success("Successfully");
        }

        public async Task<Result<string>> DeleteOneAsync(Guid id)
        {
            var eventType = await _eventTypeRepository.GetOneAsync(id);

            if (eventType == null) return null;

            var result = await _eventTypeRepository.RemoveAsync(eventType) > 0;

            if (!result) return Result<string>.Failure("Faild to edit unit");

            return Result<string>.Success("Successfully");
        }
    }
}
