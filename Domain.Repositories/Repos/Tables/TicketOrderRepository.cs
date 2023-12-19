using Domain.Models.Tables;
using Domain.Repositories.Repos.Interfaces.Tables;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.Repos.Tables
{
    public class TicketOrderRepository : BaseRepository<TicketOrder>, ITicketOrderRepository
    {
        public async Task<List<TicketOrder>> GetCustomersTicketListSortedAsync(Guid userId)
            => await Context.TicketOrder
            .Where(to => to.Order.UserId  == userId)
            .Include(to => to.Ticket)
            .Include(to => to.Order)
            .OrderByDescending(to => to.CreatedAt)
            .ToListAsync();

        public async Task<bool> HasUserAccessToTheTicketOrderAsync(Guid ticketId, Guid userId)
        {
            var ticketOrderUserId = await Context.TicketOrder
                .Where(to => to.TicketId == ticketId)
                .Select(to => to.Order.UserId)
                .FirstOrDefaultAsync();

            return ticketOrderUserId == userId;
        }

        public async Task<TicketOrder> GetCustomersTicketOrderDetailedAsync1(Guid ticketId)
            => await Context.TicketOrder
            .Where(to => to.TicketId == ticketId)
            .Include(to => to.Order)
            .Include(to => to.Ticket)
            .FirstOrDefaultAsync();

        public async Task<TicketOrder> GetCustomersTicketOrderDetailedAsync2(Guid orderId)
           => await Context.TicketOrder
           .Where(to => to.OrderId == orderId)
           .Include(to => to.Order)
           .Include(to => to.Ticket)
           .FirstOrDefaultAsync();
    }
}
