using System.ComponentModel.DataAnnotations;
using Domain.Models.Base;
using Domain.Models.Catalogues;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models.Tables
{
    public class Ticket : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Number { get; set; } = Guid.NewGuid();

        [ForeignKey(nameof(TypeId))]
        public TicketType Type { get; set; }
        public Guid TypeId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }
        public Guid EventId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(DiscountId))]
        public TicketDiscount? Discount { get; set; }
        public Guid? DiscountId { get; set; }

        public bool isPurchased { get; set; } = false;

        [Range(0, double.MaxValue)]
        public double FinalPrice { get; set; }

        public ICollection<TicketOrder> TicketOrders { get; set; }
    }
}
