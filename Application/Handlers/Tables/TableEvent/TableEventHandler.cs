using Application.Core;
using Application.DTOs.Tables;
using Application.DTOs.Users.HTTP;
using AutoMapper;
using Domain.Repositories.Repos.Interfaces.Tables;

namespace Application.Handlers.Tables.TableEvent
{
    public class TableEventHandler : ITableEventHandler
    {
        private readonly ITableEventRepository _tableEventRepository;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public TableEventHandler(ITableEventRepository tableEventRepository, IMapper mapper, IUserAccessor userAccessor)
        {
            _tableEventRepository = tableEventRepository;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<Result<List<TableEventDto>>> GetEventsTabletListAsync(Guid eventId)
        {
            var tableEvents = await _tableEventRepository.GetEventsTabletSortedListAsync(eventId);

            List<TableEventDto> tableEventDtos = new List<TableEventDto>();

            _mapper.Map(tableEvents, tableEventDtos);

            return Result<List<TableEventDto>>.Success(tableEventDtos);
        }

        public async Task<Result<TableEventDto>> GetEventsTableDetailedAsync(Guid id)
        {
            if (!await _tableEventRepository.HasUserAccessToTheEventTableAsync(id,
                    _userAccessor.GetUserId()))
            {
                return Result<TableEventDto>.Failure("You have no access to this data");
            }

            var tableEvents = await _tableEventRepository.GetEventsTableDetailedAsync(id);

            TableEventDto tableEventDtos = new TableEventDto();

            _mapper.Map(tableEvents, tableEventDtos);

            return Result<TableEventDto>.Success(tableEventDtos);
        }

        public async Task<Result<string>> CreateEventsTableAsync(TableEventDto tableEventDto)
        {
            var tableEvent = new Domain.Models.Tables.TableEvent();

            if (tableEventDto == null || tableEventDto.EventId == null || tableEventDto.TableId == null)
            {
                Result<string>.Failure("Failed to create TicketOrder. Wrong data passed.");
            }

            _mapper.Map(tableEventDto, tableEvent);

            var result = await _tableEventRepository.AddAsync(tableEvent) > 0;

            if (!result) return Result<string>.Failure("Failed to create TableEvent");

            return Result<string>.Success("Successfully");
        }

        public async Task<Result<string>> EditEventsTableAsync(TableEventDto tableEventDto)
        {
            if (tableEventDto == null || tableEventDto.EventId == null || tableEventDto.TableId == null)
            {
                Result<string>.Failure("Failed to create TicketOrder. Wrong data passed.");
            }

            var tableEvent = await _tableEventRepository.GetOneAsync(tableEventDto.Id);

            if (tableEvent == null) return null;

            if (!await _tableEventRepository.HasUserAccessToTheEventTableAsync(tableEventDto.Id.Value, _userAccessor.GetUserId()))
            {
                return Result<string>.Failure("You have no access to this data");
            }

            _mapper.Map(tableEventDto, tableEvent);

            var result = await _tableEventRepository.SaveAsync(tableEvent) > 0;

            if (!result) return Result<string>.Failure("Failed to update TableEvent");

            return Result<string>.Success("Successfully");
        }

        public async Task<Result<string>> DeleteEventsTableAsync(Guid tableEventId)
        {
            var tableEvent = await _tableEventRepository.GetOneAsync(tableEventId);

            if (tableEvent == null) return null;

            if (!await _tableEventRepository.HasUserAccessToTheEventTableAsync(tableEventId, _userAccessor.GetUserId()))
            {
                return Result<string>.Failure("You have no access to this data");
            }

            var result = await _tableEventRepository.RemoveAsync(tableEvent) > 0;

            if (!result) return Result<string>.Failure("Failed to delete TableEvent");

            return Result<string>.Success("Successfully");
        }
    }
}
