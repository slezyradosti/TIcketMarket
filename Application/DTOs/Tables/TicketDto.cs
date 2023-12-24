namespace Application.DTOs.Tables
{
    public class TicketDto: BaseDto
    {
        public Guid? Number { get; set; }
        public Guid TypeId { get; set; }
        public Guid EventId { get; set; }
        public bool? isPurchased { get; set; } = false;
        public Guid? DiscountId { get; set; }
    }
}
