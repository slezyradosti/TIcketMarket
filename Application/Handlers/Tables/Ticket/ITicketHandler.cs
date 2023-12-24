using Application.Core;
using Application.DTOs.Tables;
using Domain.Repositories.DTOs;

namespace Application.Handlers.Tables.Ticket
{
    public interface ITicketHandler
    {
        //public Task<Result<List<TicketOrderDto>>> GetCustomersTicketListAsync();
        public Task<Result<TicketDto>> GetCustomersTicketAsync(Guid ticketId);
        public Task<Result<string>> CreateCustomersOneAsync(TicketDto ticketDto);
        public Task<Result<string>> EditCustomersOneAsync(TicketDto ticketDto);
        public Task<Result<string>> DeleteCustomersOneAsync(Guid ticketId);
        public Task<Result<string>> GenerateEventsTicketList(TicketDto ticketDto, int ticketAmount);
        public Task<Result<EventTicketsAmountDto>> GetEventTicketsAmountAsync(Guid eventId);
    }
}
