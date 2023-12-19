using Application.Core;
using Application.DTOs.Tables;
using Application.DTOs.Users.HTTP;
using AutoMapper;
using Domain.Repositories.Repos.Interfaces.Tables;

namespace Application.Handlers.Tables.TicketOrder
{
    public class TicketOrderHandler : ITicketOrderHandler
    {
        private readonly ITicketOrderRepository _ticketOrderRepository;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public TicketOrderHandler(ITicketOrderRepository ticketOrderRepository, IMapper mapper, IUserAccessor userAccessor)
        {
            _ticketOrderRepository = ticketOrderRepository;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<Result<List<TicketOrderDto>>> GetCustomersTicketListAsync()
        {
            var userId = _userAccessor.GetUserId();

            var ticketOrders = await _ticketOrderRepository.GetCustomersTicketListSortedAsync(userId);

            List<TicketOrderDto> ticketOrdersDtos = new List<TicketOrderDto>();

            _mapper.Map(ticketOrders, ticketOrdersDtos);

            return Result<List<TicketOrderDto>>.Success(ticketOrdersDtos);
        }

        public async Task<Result<TicketOrderDto>> GetCustomersTicketOrderDetailedAsync(Guid ticketId)
        {
            if (!await _ticketOrderRepository.HasUserAccessToTheTicketOrderAsync(ticketId,
                    _userAccessor.GetUserId()))
            {
                return Result<TicketOrderDto>.Failure("You have no access to this data");
            }

            var ticketOrder = await _ticketOrderRepository.GetCustomersTicketOrderDetailedAsync1(ticketId);

            TicketOrderDto ticketOrderDto = new TicketOrderDto();

            _mapper.Map(ticketOrder, ticketOrderDto);

            return Result<TicketOrderDto>.Success(ticketOrderDto);
        }

        public async Task<Result<string>> CreateCustomersOneAsync(TicketOrderDto ticketOrderDto)
        {
            var ticketOrder = new Domain.Models.Tables.TicketOrder();

            if (ticketOrderDto == null || ticketOrderDto.TicketId == null || ticketOrderDto.OrderId == null) 
            {
                Result<string>.Failure("Failed to create TicketOrder. Wrong data passed.");
            }

            _mapper.Map(ticketOrderDto, ticketOrder);

            var result = await _ticketOrderRepository.AddAsync(ticketOrder) > 0;

            if (!result) return Result<string>.Failure("Failed to create TicketOrder");

            return Result<string>.Success("Successfully");
        }

        public async Task<Result<string>> EditCustomersOneAsync(TicketOrderDto ticketOrderDto)
        {
            if (ticketOrderDto == null || ticketOrderDto.TicketId == null || ticketOrderDto.OrderId == null)
            {
                Result<string>.Failure("Failed to edit TicketOrder. Wrong data passed.");
            }

            var ticketOrder = await _ticketOrderRepository.GetOneAsync(ticketOrderDto.Id);

            if (ticketOrder == null) return null;

            if (!await _ticketOrderRepository.HasUserAccessToTheTicketOrderAsync(ticketOrderDto.Id.Value, _userAccessor.GetUserId()))
            {
                return Result<string>.Failure("You have no access to this data");
            }

            _mapper.Map(ticketOrderDto, ticketOrder);

            var result = await _ticketOrderRepository.SaveAsync(ticketOrder) > 0;

            if (!result) return Result<string>.Failure("Failed to update TicketOrder");

            return Result<string>.Success("Successfully");
        }

        public async Task<Result<string>> DeleteCustomersOneAsync(Guid ticketOrderId)
        {
            var ticketOrder = await _ticketOrderRepository.GetOneAsync(ticketOrderId);

            if (ticketOrder == null) return null;

            if (!await _ticketOrderRepository.HasUserAccessToTheTicketOrderAsync(ticketOrderId, _userAccessor.GetUserId()))
            {
                return Result<string>.Failure("You have no access to this data");
            }

            var result = await _ticketOrderRepository.RemoveAsync(ticketOrder) > 0;

            if (!result) return Result<string>.Failure("Failed to delete TicketOrder");

            return Result<string>.Success("Successfully");
        }
    }
}
