using Domain.Models.Tables;
using Domain.Repositories.DTOs;
using Domain.Repositories.Repos.Interfaces.Tables;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.Repos.Tables;

public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
{
    public async Task<bool> HasUserAccessToTheEventAsync(Guid eventId, Guid userId)
    {
        var eventUserId = await Context.Ticket
            .Where(t => t.EventId == eventId)
            .Select(t => t.Event.UserId)
            .FirstOrDefaultAsync();

        return eventUserId == userId;
    }

    public async Task<bool> HasUserAccessToTheTicketAsync(Guid ticketId, Guid userId)
    {
        var ticketUserId = await Context.Ticket
            .Where(t => t.Id == ticketId)
            .Select(t => t.Event.UserId)
            .FirstOrDefaultAsync();

        return ticketUserId == userId;
    }

    public async Task<EventTicketsAmountDto> GetEventsTicketAmountAsync(Guid eventId)
    {
        return await (from t in Context.Ticket
                where t.EventId == eventId
                group t by t.EventId into groupedData
                select new EventTicketsAmountDto
                {
                    
                    Total = groupedData.Count(),
                    Purchased = groupedData.Count(t => !t.isPurchased),
                    Available = groupedData.Count(t => t.isPurchased)
                })
            
            .FirstOrDefaultAsync();
    }
    
    public async Task<Ticket> GetOneTypeIncludedAsync(Guid ticketId)
        => await Context.Ticket
            .Where(t => t.Id == ticketId)
            .Include(t => t.Type)
            .FirstOrDefaultAsync();
    
    public async Task<Ticket> GetOneDetailedAsync(Guid ticketId)
        => await Context.Ticket
            .Where(t => t.Id == ticketId)
            .Include(t => t.Type)
            .Include(t => t.Event)
            .FirstOrDefaultAsync();
    
    public async Task<List<Ticket>> GetAvailableTicketListAsync(Guid eventId)
        => await Context.Ticket
            .Where(t => t.EventId == eventId).Where(t => !t.isPurchased)
            .Include(t => t.Type)
            .ToListAsync();
    
    public async Task<Ticket?> GetOneToBuyDetailedAsync(Guid eventId, Guid typeId)
        => await Context.Ticket
            .Where(t => t.EventId == eventId && t.TypeId == typeId && !t.isPurchased)
            .Include(t => t.Type)
            .Include(t => t.Event)
            .FirstOrDefaultAsync();
}