using Domain.Models.Catalogues;
using Domain.Repositories.Repos.Interfaces.Catalogues;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.Repos.Catalogues;

public class TicketTypeRepository : BaseRepository<TicketType>, ITicketTypeRepository
{
    public async Task<double> GetPriceAsync(Guid typeId)
        => await Context.TicketType
            .Where(tt => tt.Id == typeId)
            .Select(tt => tt.Price)
            .FirstOrDefaultAsync();
}