namespace Application.DTOs.Tables
{
    public class TicketDto
    {
        public Guid? Id { get; set; }
        public int? Number { get; set; }
        public Guid TypeId { get; set; }
        public Guid EventId { get; set; }
    }
}
