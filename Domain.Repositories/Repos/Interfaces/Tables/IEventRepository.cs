using Domain.Models.Tables;

namespace Domain.Repositories.Repos.Interfaces.Tables
{
    public interface IEventRepository : IRepository<Event>
    {
        public Task<List<Event>> GetSellersEventListSortedAsync(Guid userId);
        public Task<bool> HasUserAccessToTheEventAsync(Guid eventId, Guid userId);
        public Task<int> GetOwnedCountAsync(Guid userId);
        public Task<Event> GetOneDetailedAsync(Guid eventId);
    }
}
