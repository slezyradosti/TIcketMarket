using Domain.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models.Tables
{
    public class UserEvent : BaseModel
    {
        [JsonIgnore]
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        public Guid UserId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }
        public Guid EventId { get; set; }
    }
}
