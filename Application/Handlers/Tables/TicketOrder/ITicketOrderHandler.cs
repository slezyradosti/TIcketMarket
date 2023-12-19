using Application.Core;
using Application.DTOs.Tables;

namespace Application.Handlers.Tables.TicketOrder
{
    public interface ITicketOrderHandler
    {
        public Task<Result<List<TicketOrderDto>>> GetCustomersTicketListAsync();
        public Task<Result<TicketOrderDto>> GetCustomersTicketOrderDetailedAsync(Guid ticketId);
        public Task<Result<string>> CreateCustomersOneAsync(TicketOrderDto ticketOrderDto);
        public Task<Result<string>> EditCustomersOneAsync(TicketOrderDto ticketOrderDto);
        public Task<Result<string>> DeleteCustomersOneAsync(Guid ticketOrderId);
    }
}
