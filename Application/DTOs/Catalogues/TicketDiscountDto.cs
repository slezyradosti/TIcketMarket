using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Catalogues;

public class TicketDiscountDto : BaseDto
{
    [Range(0, 100)] public int DiscountPercentage { get; set; }
}