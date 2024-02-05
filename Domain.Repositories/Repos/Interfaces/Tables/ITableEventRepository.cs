using Domain.Models.Tables;

namespace Domain.Repositories.Repos.Interfaces.Tables;

public interface ITableEventRepository : IRepository<TableEvent>
{
    public Task<List<TableEvent>> GetEventsTabletSortedListAsync(Guid eventId);
    public Task<bool> HasUserAccessToTheEventTableByEventAsync(Guid eventId, Guid userId);
    public Task<TableEvent> GetEventsTableDetailedByEventAsync(Guid eventId);
    public Task<TableEvent> GetEventsTableDetailedAsync(Guid id);
    public Task<bool> HasUserAccessToTheEventTableAsync(Guid id, Guid userId);
}