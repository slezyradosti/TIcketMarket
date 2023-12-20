using Domain.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models.Tables
{
    public class Order : BaseModel
    {
        [JsonIgnore]
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        public Guid UserId { get; set; }

        public ICollection<TicketOrder> TicketOrders { get; set; }
    }
}
