using Application.Core;
using Application.DTOs.Tables;

namespace Application.Handlers.Tables.Order
{
    public interface IOrderHandler
    {
        public Task<Result<List<OrderDto>>> GetCustomersOrderListAsync();
        public Task<Result<OrderDto>> GetCustomersOrderAsync(Guid eventId);
        public Task<Result<string>> CreateCustomersOneAsync(OrderDto OrderDto);
        public Task<Result<string>> DeleteCustomersOneAsync(Guid eventId);
    }
}
