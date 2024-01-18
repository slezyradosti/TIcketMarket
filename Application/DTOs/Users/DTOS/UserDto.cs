using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Users.DTOS
{
    public class UserDto
    {
        public Guid? Id { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }

        [StringLength(60)]
        public string Firstname { get; set; }

        [StringLength(75)]
        public string Lastname { get; set; }
        public DateTime DOB { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }
        
        public string Email { get; set; }
    }
}
