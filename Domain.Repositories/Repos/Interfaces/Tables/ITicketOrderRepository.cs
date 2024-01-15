using Domain.Models.Tables;

namespace Domain.Repositories.Repos.Interfaces.Tables;

public interface ITicketOrderRepository : IRepository<TicketOrder>
{
    public Task<List<TicketOrder>> GetCustomersTicketOrderListSortedAsync(Guid userId);
    public Task<bool> HasUserAccessToTheTicketOrderAsync(Guid ticketId, Guid userId);
    public Task<TicketOrder> GetCustomersTicketOrderDetailedAsync1(Guid ticketId);
    public Task<TicketOrder> GetCustomersTicketOrderDetailedAsync2(Guid orderId);
    public Task<List<Ticket>> GetCustomersTicketListSortedAsync(Guid userId);
}