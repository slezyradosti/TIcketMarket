using Domain.Models.Tables;
using Domain.Repositories.DTOs;
using Domain.Repositories.Repos.Interfaces.Tables;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.Repos.Tables;

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
    
    public async Task<int> GetEventsTotalPlacesAsync(Guid eventId)
        => await Context.Event
            .Where(e => e.Id == eventId)
            .Select(e => e.TotalPlaces)
            .FirstOrDefaultAsync();
    
    public async Task<List<Event>> GetAllEventsOrderedAsync()
        => await Context.Event
            .Include(e => e.Type)
            .Include(e => e.Category)
            .OrderBy(e => e.CreatedAt)
            .ToListAsync();
    
    public async Task<List<EventExtendedDto>> GetAllExtendedEventsOrderedAsync()
        => await Context.Event
            .Include(e => e.Type)
            .Include(e => e.Category)
            .Select(e => new EventExtendedDto()
            {
                Id = e.Id,
                Category = e.Category,
                TotalPlaces = e.TotalPlaces,
                Date = e.Date,
                Description = e.Description,
                Moderator = e.Moderator,
                Place = e.Place,
                Title = e.Title,
                Type = e.Type,
                User = e.User,
                UserId = e.UserId,
                CategoryId = e.CategoryId,
                TypeId = e.TypeId,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt,
                TotalTickets = e.Tickets.Count(),
                AvailableTickets = e.Tickets.Count(t => !t.isPurchased),
                PurchasedTickets = e.Tickets.Count(t => t.isPurchased),
            })
            .OrderBy(e => e.CreatedAt)
            .ToListAsync();
}