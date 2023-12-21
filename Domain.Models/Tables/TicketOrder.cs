using Domain.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models.Tables
{
    public class TicketOrder : BaseModel
    {
        [JsonIgnore]
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }
        public Guid? OrderId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(TicketId))]
        public Ticket? Ticket { get; set; }
        public Guid? TicketId { get; set; }
    }
}
