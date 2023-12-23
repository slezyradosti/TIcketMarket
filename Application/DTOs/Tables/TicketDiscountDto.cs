using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Catalogues;

public class TicketDiscountDto : BaseDto
{
    [Range(0, 100)] public int DiscountPercentage { get; set; }
    public string Code { get; set; }
    public bool? isActivated { get; set; } = false;
    public Guid? UserId { get; set; }
}