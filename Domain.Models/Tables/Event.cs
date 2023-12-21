using Domain.Models.Base;
using Domain.Models.Catalogues;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models.Tables
{
    public class Event : BaseModel
    {
        [StringLength(75)]
        public string Title { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(CategoryId))]
        public EventCategory Category { get; set; }
        public Guid CategoryId { get; set; } // *
        public string Description { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        public Guid UserId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(TypeId))]
        public EventType Type { get; set; }
        public Guid TypeId { get; set; } // * offline, online...

        //public Guid? ModeratorId { get; set; } // * 
        public string Moderator { get; set; }
        public int TotalPlaces { get; set; }
        public ICollection<TableEvent> TableEvents { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
