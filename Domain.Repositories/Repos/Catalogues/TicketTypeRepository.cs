using System.Data.Entity;
using Domain.Models.Catalogues;
using Domain.Models.Tables;
using Domain.Repositories.Repos.Interfaces.Catalogues;

namespace Domain.Repositories.Repos.Catalogues;

public class TicketTypeRepository : BaseRepository<TicketType>, ITicketTypeRepository
{
    public async Task<double> GetPriceAsync(Guid typeId)
        => await Context.TicketType
            .Where(tt => tt.Id == typeId)
            .Select(tt => tt.Price)
            .FirstOrDefaultAsync();
}