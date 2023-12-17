using Application.Core;
using Application.DTOs.Catalogues;
using AutoMapper;
using Domain.Repositories.Repos.Interfaces.Catalogues;

namespace Application.Handlers.Catalogues.EventCategory
{
    public class EventCategoryHandler : IEventCategoryHandler
    {
        private readonly IEventCategoryRepository _eventCategoryRepository;
        private readonly IMapper _mapper;

        public EventCategoryHandler(IEventCategoryRepository eventCategoryRepository, IMapper mapper)
        {
            _eventCategoryRepository = eventCategoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<EventCategoryDto>>> GetListAsync()
        {
            var eventCategories = await _eventCategoryRepository.GetAllAsync();

            List<EventCategoryDto> EventCategoryDtos = new List<EventCategoryDto>();

            _mapper.Map(eventCategories, EventCategoryDtos);

            return Result<List<EventCategoryDto>>.Success(EventCategoryDtos);
        }

        public async Task<Result<EventCategoryDto>> GetOneAsync(Guid eventId)
        {
            var eventCategory = await _eventCategoryRepository.GetOneAsync(eventId);

            EventCategoryDto EventCategoryDto = new EventCategoryDto();

            _mapper.Map(eventCategory, EventCategoryDto);

            return Result<EventCategoryDto>.Success(EventCategoryDto);
        }

        public async Task<Result<string>> CreateOneAsync(EventCategoryDto EventCategoryDto)
        {
            var eventCategory = new Domain.Models.Catalogues.EventCategory();
            _mapper.Map(EventCategoryDto, eventCategory);

            var result = await _eventCategoryRepository.AddAsync(eventCategory) > 0;

            if (!result) return Result<string>.Failure("Failed to create Event Category");

            return Result<string>.Success("Successfully");
        }

        public async Task<Result<string>> EditOneAsync(EventCategoryDto EventCategoryDto)
        {
            var eventCategory = await _eventCategoryRepository.GetOneAsync(EventCategoryDto.Id);

            if (eventCategory == null) return null;

            _mapper.Map(EventCategoryDto, eventCategory);

            var result = await _eventCategoryRepository.SaveAsync(eventCategory) > 0;

            if (!result) return Result<string>.Failure("Faild to edit unit");

            return Result<string>.Success("Successfully");
        }

        public async Task<Result<string>> DeleteOneAsync(Guid id)
        {
            var eventCategory = await _eventCategoryRepository.GetOneAsync(id);

            if (eventCategory == null) return null;

            var result = await _eventCategoryRepository.RemoveAsync(eventCategory) > 0;

            if (!result) return Result<string>.Failure("Faild to edit unit");

            return Result<string>.Success("Successfully");
        }
    }
}
