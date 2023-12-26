using Domain.Models.Catalogues;
using Domain.Models.Tables;

namespace Domain.Repositories.Repos.Interfaces.Catalogues;

public interface ITicketTypeRepository : IRepository<TicketType>
{
    public Task<double> GetPriceAsync(Guid typeId);
}