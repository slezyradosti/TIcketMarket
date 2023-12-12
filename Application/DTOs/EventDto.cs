using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class EventDto
    {
        public Guid? Id { get; set; }

        [StringLength(75)]
        public string Title { get; set; }
        public Guid CategoryId { get; set; } // *
        public string Description { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public Guid TypeId { get; set; } // * offline, online...
        public string Moderator { get; set; }
        public int TotalPlaces { get; set; }
        public int? FreePlaces { get; set; }

        [Range(0, (double)decimal.MaxValue)]
        public decimal TicketPrice { get; set; }

        [Range(0, 100)]
        public int TicketDiscountPercentage { get; set; }

    }
}
