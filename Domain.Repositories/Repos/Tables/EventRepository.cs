using Domain.Models.Tables;
using Domain.Repositories.Repos.Interfaces.Tables;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.Repos.Tables
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public async Task<List<Event>> GetSellersEventListSortedAsync(Guid userId)
            => await Context.Event
            .Where(e => e.UserId == userId)
            .Include(e => e.Type)
            .Include(e => e.Category)
            .OrderBy(e => e.CreatedAt)
            .ToListAsync();

        public async Task<bool> HasUserAccessToTheEventAsync(Guid eventId, Guid userId)
        {
            var eventUserId = await Context.Event
                .Where(e => e.Id == eventId)
                .Select(e => e.UserId)
                .FirstOrDefaultAsync();

            return eventUserId == userId;
        }

        public async Task<int> GetOwnedCountAsync(Guid userId)
            => await Context.Event
                .Where(e => e.UserId == userId)
                .CountAsync();

        public async Task<Event> GetOneDetailedAsync(Guid eventId)
            => await Context.Event
                .Where(e => e.Id == eventId)
                .FirstOrDefaultAsync();
    }
}
