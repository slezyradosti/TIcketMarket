using System.ComponentModel.DataAnnotations;
using Domain.Models.Base;
using Domain.Models.Tables;

namespace Domain.Models.Catalogues;

public class TicketDiscount : BaseModel
{
    [Range(0, 100)]
    public int DiscountPercentage { get; set; }
    
    public ICollection<Ticket> Tickets { get; set; }
}