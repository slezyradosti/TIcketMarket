using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Tables
{
    public class TicketDto
    {
        public Guid? Id { get; set; }

        [Range(0, (double)decimal.MaxValue)]
        public decimal? TicketPrice { get; set; } // will count from EventTicketPrice: price - discount

        // ticket typeId
    }
}
