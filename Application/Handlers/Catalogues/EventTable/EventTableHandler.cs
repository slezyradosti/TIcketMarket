using Application.Core;
using Application.DTOs.Catalogues;
using AutoMapper;
using Domain.Repositories.Repos.Interfaces.Catalogues;

namespace Application.Handlers.Catalogues.EventTable
{
    public class EventTableHandler : IEventTableHandler
    {
        private readonly IEventTableRepository _eventTableRepository;
        private readonly IMapper _mapper;

        public EventTableHandler(IEventTableRepository eventTableRepository, IMapper mapper)
        {
            _eventTableRepository = eventTableRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<EventTableDto>>> GetListAsync()
        {
            var eventTables = await _eventTableRepository.GetAllAsync();

            List<EventTableDto> EventTableDtos = new List<EventTableDto>();

            _mapper.Map(eventTables, EventTableDtos);

            return Result<List<EventTableDto>>.Success(EventTableDtos);
        }

        public async Task<Result<EventTableDto>> GetOneAsync(Guid eventId)
        {
            var eventTable = await _eventTableRepository.GetOneAsync(eventId);

            EventTableDto EventTableDto = new EventTableDto();

            _mapper.Map(eventTable, EventTableDto);

            return Result<EventTableDto>.Success(EventTableDto);
        }

        public async Task<Result<string>> CreateOneAsync(EventTableDto EventTableDto)
        {
            var eventTable = new Domain.Models.Catalogues.EventTable();
            _mapper.Map(EventTableDto, eventTable);

            var result = await _eventTableRepository.AddAsync(eventTable) > 0;

            if (!result) return Result<string>.Failure("Failed to create Event Table");

            return Result<string>.Success("Successfully");
        }

        public async Task<Result<string>> EditOneAsync(EventTableDto EventTableDto)
        {
            var eventTable = await _eventTableRepository.GetOneAsync(EventTableDto.Id);

            if (eventTable == null) return null;

            _mapper.Map(EventTableDto, eventTable);

            var result = await _eventTableRepository.SaveAsync(eventTable) > 0;

            if (!result) return Result<string>.Failure("Faild to edit Event Table");

            return Result<string>.Success("Successfully");
        }

        public async Task<Result<string>> DeleteOneAsync(Guid id)
        {
            var eventTable = await _eventTableRepository.GetOneAsync(id);

            if (eventTable == null) return null;

            var result = await _eventTableRepository.RemoveAsync(eventTable) > 0;

            if (!result) return Result<string>.Failure("Faild to edit Event Table");

            return Result<string>.Success("Successfully");
        }
    }
}
