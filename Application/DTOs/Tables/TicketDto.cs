using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Tables
{
    public class TicketDto: BaseDto
    {
        public Guid? Number { get; set; }
        public Guid TypeId { get; set; }
        public Guid EventId { get; set; }
        public bool? isPurchased { get; set; } = false;
        
        [Range(0, double.MaxValue)]
        public double FinalPrice { get; set; }
        
        public Guid? DiscountId { get; set; }
    }
}
