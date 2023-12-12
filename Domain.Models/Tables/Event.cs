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
        [ForeignKey(nameof(TypeId))]
        public EventType Type { get; set; }
        public Guid TypeId { get; set; } // * offline, online...

        //public Guid? ModeratorId { get; set; } // * 
        public string Moderator { get; set; }
        public int TotalPlaces { get; set; }
        public int? FreePlaces { get; set; } // when creating set this value equal to TotalPlaces (!! only if the value is null)

        [Range(0, (double)decimal.MaxValue)]
        public decimal TicketPrice { get; set; }

        [Range(0, 100)]
        public int TicketDiscountPercentage { get; set; }

        public ICollection<TableEvent> TableEvents { get; set; }

    }
}
