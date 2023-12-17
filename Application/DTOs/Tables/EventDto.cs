using Domain.Models.Tables;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Tables
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
        public Guid? UserId { get; set; }
        public Guid TypeId { get; set; } // * offline, online...
        public string Moderator { get; set; }
        public int TotalPlaces { get; set; }
        public int? FreePlaces { get; set; } // when creating set this value equal to TotalPlaces (!! only if the value is null)

        [Range(0, double.MaxValue)]
        public double TicketPrice { get; set; }

        [Range(0, 100)]
        public int TicketDiscountPercentage { get; set; }
    }
}
