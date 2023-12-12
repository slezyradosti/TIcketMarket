using Domain.Models.Base;
using Domain.Models.Tables;

namespace Domain.Models.Catalogues
{
    public class EventCategory : BaseModel
    {
        public string Category { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
