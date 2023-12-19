using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Catalogues
{
    public class EventTableDto
    {
        public Guid? Id { get; set; }
        public string Number { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Range(0, int.MaxValue)]
        public int PeopleQuantity { get; set; }
    }
}
