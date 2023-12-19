using Application.Core;
using Application.DTOs.Tables;
using Application.DTOs.Users.HTTP;
using AutoMapper;
using Domain.Repositories.Repos.Interfaces.Tables;

namespace Application.Handlers.Tables.Order
{
    public class OrderHandler : IOrderHandler
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public OrderHandler(IOrderRepository orderRepository, IMapper mapper, IUserAccessor userAccessor)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<Result<List<OrderDto>>> GetCustomersOrderListAsync()
        {
            var userId = _userAccessor.GetUserId();

            var orders = await _orderRepository.GetCustomersListSortedAsync(userId);

            List<OrderDto> OrderDtos = new List<OrderDto>();

            _mapper.Map(orders, OrderDtos);

            return Result<List<OrderDto>>.Success(OrderDtos);
        }

        public async Task<Result<OrderDto>> GetCustomersOrderAsync(Guid eventId)
        {
            if (!await _orderRepository.HasUserAccessToTheOrderAsync(eventId,
                    _userAccessor.GetUserId()))
            {
                return Result<OrderDto>.Failure("You have no access to this data");
            }

            var order = await _orderRepository.GetOneAsync(eventId);

            OrderDto OrderDto = new OrderDto();

            _mapper.Map(order, OrderDto);

            return Result<OrderDto>.Success(OrderDto);
        }

        public async Task<Result<string>> CreateCustomersOneAsync(OrderDto OrderDto)
        {
            OrderDto.UserId = _userAccessor.GetUserId();

            var order = new Domain.Models.Tables.Order();
            _mapper.Map(OrderDto, order);

            var result = await _orderRepository.AddAsync(order) > 0;

            if (!result) return Result<string>.Failure("Failed to create Order");

            return Result<string>.Success("Successfully");
        }

        //public async Task<Result<string>> EditSellersOneAsync(OrderDto OrderDto)
        //{
        //    var order = await _eventRepository.GetOneAsync(OrderDto.Id);

        //    if (order == null) return null;

        //    var userId = _userAccessor.GetUserId();

        //    if (!await _eventRepository.HasUserAccessToTheEvent(OrderDto.Id.Value, userId))
        //    {
        //        return Result<string>.Failure("You have no access to this data");
        //    }

        //    OrderDto.UserId ??= userId;
        //    _mapper.Map(OrderDto, order);

        //    var result = await _eventRepository.SaveAsync(order) > 0;

        //    if (!result) return Result<string>.Failure("Failed to update Event");

        //    return Result<string>.Success("Successfully");
        //}

        public async Task<Result<string>> DeleteSellersOneAsync(Guid eventId)
        {
            var order = await _orderRepository.GetOneAsync(eventId);

            if (order == null) return null;

            if (!await _orderRepository.HasUserAccessToTheOrderAsync(eventId, _userAccessor.GetUserId()))
            {
                return Result<string>.Failure("You have no access to this data");
            }

            var result = await _orderRepository.RemoveAsync(order) > 0;

            if (!result) return Result<string>.Failure("Failed to delete Order");

            return Result<string>.Success("Successfully");
        }
    }
}
