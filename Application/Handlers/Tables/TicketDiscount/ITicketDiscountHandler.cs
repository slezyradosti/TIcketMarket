using Application.Core;
using Application.DTOs.Catalogues;

namespace Application.Handlers.Catalogues.TicketDiscount;

public interface ITicketDiscountHandler
{
    public Task<Result<List<TicketDiscountDto>>> GetSellersTicketDiscountListAsync();
    public Task<Result<TicketDiscountDto>> GetSellersDiscountAsync(Guid ticketId);
    public Task<Result<string>> CreateSellersOneAsync(TicketDiscountDto ticketDiscountDto);
    public Task<Result<string>> EditSellersOneAsync(TicketDiscountDto ticketDiscountDto);
    public Task<Result<string>> DeleteSellersOneAsync(Guid ticketDiscountId);
    public Task<Result<Domain.Models.Catalogues.TicketDiscount>> ActiveDiscount(string discountCode);
    public Task<Result<Domain.Models.Catalogues.TicketDiscount>> DeactivateDiscount(Guid discountId);
}