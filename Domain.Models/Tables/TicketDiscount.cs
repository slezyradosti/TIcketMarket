using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Models.Base;
using Domain.Models.Tables;

namespace Domain.Models.Catalogues;

public class TicketDiscount : BaseModel
{
    [Range(0, 100)]
    public int DiscountPercentage { get; set; }
    public string Code { get; set; }
    public bool? isActivated { get; set; } = false;
    
    [JsonIgnore]
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }
    public Guid UserId { get; set; }
    
    public ICollection<Ticket> Tickets { get; set; }
}