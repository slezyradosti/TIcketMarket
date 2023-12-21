using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Tables
{
    public class EventDto: BaseDto
    {
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
    }
}
