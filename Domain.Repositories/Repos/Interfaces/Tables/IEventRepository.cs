using Domain.Models.Catalogues;
using Domain.Models.Tables;

namespace Domain.Repositories.Repos.Interfaces.Tables
{
    public interface IEventRepository : IRepository<Event>
    {
        public Task<List<Event>> GetSellerEventsSorted(Guid userId);
        public Task<bool> HasUserAccessToTheEvent(Guid eventId, Guid userId);
        public Task<int> GetOwnedCountAsync(Guid userId);
        public Task<Event> GetOneDetailedAsync(Guid eventId);
    }
}
