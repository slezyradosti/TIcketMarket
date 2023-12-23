using Domain.Models.Catalogues;

namespace Domain.Repositories.Repos.Interfaces.Catalogues;

public interface ITicketDiscountRepository : IRepository<TicketDiscount>
{
    public Task<List<TicketDiscount>> GetSelletsDiscountListSortedAsync(Guid userId);
}