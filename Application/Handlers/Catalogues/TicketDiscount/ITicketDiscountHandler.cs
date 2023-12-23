using Application.Core;
using Application.DTOs.Catalogues;

namespace Application.Handlers.Catalogues.TicketDiscount;

public interface ITicketDiscountHandler
{
    public Task<Result<List<TicketDiscountDto>>> GetCustomersTicketDiscountListAsync();
    public Task<Result<string>> CreateSellersOneAsync(TicketDiscountDto ticketDiscountDto);
}