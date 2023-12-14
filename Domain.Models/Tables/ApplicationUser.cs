using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Tables
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [StringLength(60)]
        public string Firstname { get; set; }

        [StringLength(75)]
        public string Lastname { get; set; }
        public DateTime DOB { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
