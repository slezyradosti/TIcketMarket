using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Catalogues
{
    public class TicketTypeDto : BaseDto
    {
        public string Type { get; set; }
        
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
    }
}
