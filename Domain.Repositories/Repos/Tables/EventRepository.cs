using Domain.Models.Tables;
using Domain.Repositories.Repos.Interfaces.Tables;

namespace Domain.Repositories.Repos.Tables
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
    }
}
