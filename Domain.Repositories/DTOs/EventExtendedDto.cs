using Domain.Models.Tables;

namespace Domain.Repositories.DTOs;

public class EventExtendedDto : Event
{
    public int TotalTickets { get; set; } = 0;
    public int AvailableTickets { get; set; } = 0;
    public int PurchasedTickets { get; set; } = 0;
}