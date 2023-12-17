using Application.Core;
using Application.DTOs.Catalogues;
using AutoMapper;
using Domain.Models.Catalogues;
using Domain.Repositories.Repos.Catalogues;
using Domain.Repositories.Repos.Interfaces.Catalogues;

namespace Application.Handlers.Catalogues.TicketType
{
    public class TicketTypeHandler : ITicketTypeHandler
    {
        private readonly ITicketTypeRepository _ticketTypeRepository;
        private readonly IMapper _mapper;

        public TicketTypeHandler(ITicketTypeRepository ticketTypeRepository, IMapper mapper)
        {
            _ticketTypeRepository = ticketTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<TicketTypeDto>>> GetListAsync()
        {
            var ticketTypes = await _ticketTypeRepository.GetAllAsync();

            List<TicketTypeDto> TicketTypeDtos = new List<TicketTypeDto>();

            _mapper.Map(ticketTypes, TicketTypeDtos);

            return Result<List<TicketTypeDto>>.Success(TicketTypeDtos);
        }

        public async Task<Result<TicketTypeDto>> GetOneAsync(Guid eventId)
        {
            var ticketType = await _ticketTypeRepository.GetOneAsync(eventId);

            TicketTypeDto TicketTypeDto = new TicketTypeDto();

            _mapper.Map(ticketType, TicketTypeDto);

            return Result<TicketTypeDto>.Success(TicketTypeDto);
        }

        public async Task<Result<string>> CreateOneAsync(TicketTypeDto TicketTypeDto)
        {
            var ticketType = new Domain.Models.Catalogues.TicketType();
            _mapper.Map(TicketTypeDto, ticketType);

            var result = await _ticketTypeRepository.AddAsync(ticketType) > 0;

            if (!result) return Result<string>.Failure("Failed to create Event Category");

            return Result<string>.Success("Successfully");
        }

        public async Task<Result<string>> EditOneAsync(TicketTypeDto TicketTypeDto)
        {
            var ticketType = await _ticketTypeRepository.GetOneAsync(TicketTypeDto.Id);
            if (ticketType == null) return null;

            _mapper.Map(TicketTypeDto, ticketType);

            var result = await _ticketTypeRepository.SaveAsync(ticketType) > 0;

            if (!result) return Result<string>.Failure("Faild to edit unit");

            return Result<string>.Success("Successfully");
        }

        public async Task<Result<string>> DeleteOneAsync(Guid id)
        {
            var ticketType = await _ticketTypeRepository.GetOneAsync(id);

            if (ticketType == null) return null;

            var result = await _ticketTypeRepository.RemoveAsync(ticketType) > 0;

            if (!result) return Result<string>.Failure("Faild to edit unit");

            return Result<string>.Success("Successfully");
        }
    }
}
