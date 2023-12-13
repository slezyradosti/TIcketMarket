using Domain.Models.Tables;
using Domain.Repositories.Repos.Interfaces.Tables;

namespace Domain.Repositories.Repos.Tables
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        //public async Task<List<Event>> GetSellersSortedAsync(Guid userId)
        //{
        //    var query = Context.Event
        //        .Where(x => x. == userId)
        //        .OrderBy(")
        //        .Skip(filter.PageIndex * filter.PageSize)
        //        .Take(filter.PageSize);

        //    return await query.ToListAsync();
        //}
    }
}
