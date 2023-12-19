using Domain.Models.Tables;

namespace Application.DTOs.Tables
{
    public class TicketOrderDto
    {
        public Guid? Id { get; set; }
        public Order? Order { get; set; }
        public Guid? OrderId { get; set; }

        public Ticket? Ticket { get; set; }
        public Guid? TicketId { get; set; }
    }
}
