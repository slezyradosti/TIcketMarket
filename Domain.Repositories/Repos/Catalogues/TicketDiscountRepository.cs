using Domain.Models.Catalogues;
using Domain.Repositories.Repos.Interfaces.Catalogues;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.Repos.Catalogues;

public class TicketDiscountRepository : BaseRepository<TicketDiscount>, ITicketDiscountRepository
{
    public async Task<List<TicketDiscount>> GetSelletsDiscountListSortedAsync(Guid userId)
    {
        return await (from td in Context.TicketDiscount
                join t in Context.Ticket
                    on td.Id equals t.DiscountId
                where t.Event.UserId == userId
                select new TicketDiscount
                {
                    Id = td.Id,
                    DiscountPercentage = td.DiscountPercentage
                })
            .Distinct()
            .ToListAsync();
    }
}