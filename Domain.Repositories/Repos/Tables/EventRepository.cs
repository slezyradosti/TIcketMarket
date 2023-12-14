using Domain.Models.Tables;
using Domain.Repositories.Repos.Interfaces.Tables;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.Repos.Tables
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public async Task<List<Event>> GetSellerEventsSorted(Guid userId)
            => await Context.Event
            .Where(e => e.UserId == userId)
            .Include(e => e.Type)
            .Include(e => e.Category)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();
    }
}
