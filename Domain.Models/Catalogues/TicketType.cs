using Domain.Models.Base;
using Domain.Models.Tables;

namespace Domain.Models.Catalogues
{
    public class TicketType : BaseModel
    {
        public string Type { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
