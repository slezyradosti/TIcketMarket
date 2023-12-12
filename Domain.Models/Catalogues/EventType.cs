using Domain.Models.Base;
using Domain.Models.Tables;

namespace Domain.Models.Catalogues
{
    public class EventType : BaseModel
    {
        public string Type { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
