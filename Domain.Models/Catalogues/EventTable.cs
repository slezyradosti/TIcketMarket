using Domain.Models.Base;
using Domain.Models.Tables;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Catalogues
{
    public class EventTable : BaseModel
    {
        public string Number { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Range(0, int.MaxValue)]
        public int PeopleQuantity { get; set; }

        // photo

        public ICollection<TableEvent> TableEvents { get; set; }
    }
}
