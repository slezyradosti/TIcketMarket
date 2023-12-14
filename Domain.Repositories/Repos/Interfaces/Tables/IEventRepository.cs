using Domain.Models.Catalogues;
using Domain.Models.Tables;

namespace Domain.Repositories.Repos.Interfaces.Tables
{
    public interface IEventRepository : IRepository<Event>
    {
        public Task<List<Event>> GetSellerEventsSorted(Guid userId);
    }
}
