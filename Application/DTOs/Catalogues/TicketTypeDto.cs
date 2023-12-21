using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Catalogues
{
    public class TicketTypeDto : BaseDto
    {
        [Required]
        public string Type { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
    }
}
