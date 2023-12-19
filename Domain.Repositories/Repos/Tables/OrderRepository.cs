using Domain.Models.Tables;
using Domain.Repositories.Repos.Interfaces.Tables;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.Repos.Tables
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public async Task<List<Order>> GetCustomersListSortedAsync(Guid userId)
            => await Context.Order
            .Where(o => o.UserId == userId)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();

        public async Task<bool> HasUserAccessToTheOrderAsync(Guid orderId, Guid userId)
        {
            var orderUserId = await Context.Order
                .Where(o => o.Id == orderId)
                .Select(o => o.UserId)
                .FirstOrDefaultAsync();

            return orderUserId == userId;
        }
    }
}
