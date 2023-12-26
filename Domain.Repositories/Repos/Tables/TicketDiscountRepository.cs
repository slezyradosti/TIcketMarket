using Domain.Models.Catalogues;
using Domain.Repositories.Repos.Interfaces.Catalogues;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.Repos.Tables;

public class TicketDiscountRepository : BaseRepository<TicketDiscount>, ITicketDiscountRepository
{
    public async Task<List<TicketDiscount>> GetSelletsDiscountListSortedAsync(Guid userId)
        => await Context.TicketDiscount
            .Where(td => td.UserId == userId)
            .OrderBy(td => td.CreatedAt)
            .ToListAsync();
    
     // => await (from td in Context.TicketDiscount
     //            join t in Context.Ticket
     //                on td.Id equals t.DiscountId
     //            where t.Event.UserId == userId
     //            select new TicketDiscount
     //            {
     //                Id = td.Id,
     //                DiscountPercentage = td.DiscountPercentage
     //            })
     //        .Distinct()
     //        .ToListAsync();
    
    public async Task<bool> HasUserAccessToTheTicketDiscountAsync(Guid discountId, Guid userId)
    {
        // var ticketDiscountUserId = await (from td in Context.TicketDiscount
        //         join t in Context.Ticket
        //             on td.Id equals t.DiscountId
        //             where td.Id == discountId
        //             select t.Event.UserId).FirstOrDefaultAsync();

        var ticketDiscountUserId = await Context.TicketDiscount
            .Where(td => td.Id == discountId)
            .Select(td => td.UserId)
            .FirstOrDefaultAsync();

        return ticketDiscountUserId == userId;
    }
    
    public async Task<TicketDiscount> GetDiscountByCodeAsync(string code)
        => await Context.TicketDiscount
            .FirstOrDefaultAsync();
    
    public async Task<int> GetDiscountPercentageAsync(Guid discountId)
        => await Context.TicketDiscount
            .Where(td => td.Id == discountId)
            .Select(td => td.DiscountPercentage)
            .FirstOrDefaultAsync();
}