using System.ComponentModel.DataAnnotations;
using Domain.Models.Base;
using Domain.Models.Tables;

namespace Domain.Models.Catalogues
{
    public class TicketType : BaseModel
    {
        public string Type { get; set; }
        
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
