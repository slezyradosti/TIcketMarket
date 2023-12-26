namespace Application.DTOs.Requests;

public class ApplyDiscountDto
{
    public Guid TicketId { get; set; }
    public string DiscountCode { get; set; }
}