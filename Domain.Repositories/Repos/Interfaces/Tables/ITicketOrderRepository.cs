using Domain.Models.Tables;

namespace Domain.Repositories.Repos.Interfaces.Tables
{
    public interface ITicketOrderRepository : IRepository<TicketOrder>
    {
        public Task<List<TicketOrder>> GetCustomersTicketListSortedAsync(Guid userId);
        public Task<bool> HasUserAccessToTheTicketOrderAsync(Guid ticketId, Guid userId);
    }
}
