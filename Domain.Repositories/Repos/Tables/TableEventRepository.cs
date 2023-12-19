using Domain.Models.Tables;
using Domain.Repositories.Repos.Interfaces.Tables;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.Repos.Tables
{
    public class TableEventRepository : BaseRepository<TableEvent>, ITableEventRepository
    {
        public async Task<List<TableEvent>> GetEventsTabletSortedListAsync(Guid eventId)
            => await Context.TableEvent
            .Where(te => te.EventId == eventId)
            .Include(te => te.Table)
            .OrderBy(te => te.CreatedAt)
            .ToListAsync();

        public async Task<bool> HasUserAccessToTheEventTableAsync(Guid eventId, Guid userId)
        {
            var tableEventUserId = await Context.TableEvent
                .Where(te => te.EventId == eventId)
                .Select(te => te.Event.UserId)
                .FirstOrDefaultAsync();

            return tableEventUserId == userId;
        }

        public async Task<TableEvent> GetEventsTableDetailedAsync(Guid eventId)
            => await Context.TableEvent
            .Where(to => to.EventId == eventId)
            .Include(to => to.Table)
            .FirstOrDefaultAsync();
    }
}
