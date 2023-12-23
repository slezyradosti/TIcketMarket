using Application.Core;
using Application.DTOs.Catalogues;

namespace Application.Handlers.Catalogues.TicketDiscount;

public interface ITicketDiscountHandler
{
    public Task<Result<List<TicketDiscountDto>>> GetCustomersTicketDiscountListAsync();
    public Task<Result<TicketDiscountDto>> GetCustomersDiscountAsync(Guid ticketId);
    public Task<Result<string>> CreateSellersOneAsync(TicketDiscountDto ticketDiscountDto);
    public Task<Result<string>> EditCustomersOneAsync(TicketDiscountDto ticketDiscountDto);
    public Task<Result<string>> DeleteCustomersOneAsync(Guid ticketDiscountId);
}