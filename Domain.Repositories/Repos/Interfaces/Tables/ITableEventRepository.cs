using Domain.Models.Tables;

namespace Domain.Repositories.Repos.Interfaces.Tables;

public interface ITableEventRepository : IRepository<TableEvent>
{
    public Task<List<TableEvent>> GetEventsTabletSortedListAsync(Guid eventId);
    public Task<bool> HasUserAccessToTheEventTableAsync(Guid eventId, Guid userId);
    public Task<TableEvent> GetEventsTableDetailedAsync(Guid eventId);
}