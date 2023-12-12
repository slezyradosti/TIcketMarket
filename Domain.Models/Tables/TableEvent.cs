using Domain.Models.Base;
using Domain.Models.Catalogues;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models.Tables
{
    public class TableEvent : BaseModel
    {
        [JsonIgnore]
        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }
        public Guid EventId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(TableId))]
        public EventTable Table { get; set; }
        public Guid TableId { get; set; }
    }
}
