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
    }
}
