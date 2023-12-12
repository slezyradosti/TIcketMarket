using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Users.DTOS
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        //[RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,22}$", ErrorMessage = "Password must contain a lowercase letter, a capital letter, " +
        //    "a number and a special symbol. Character range: 4-22")]
        public string Password { get; set; }

        [Required]
        [MaxLength(60)]
        public string Username { get; set; }

        [Required]
        [StringLength(60)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(75)]
        public string Lastname { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        [StringLength(50)]
        public string Phone { get; set; }

        [Required]
        public bool isCustomer { get; set; }
    }
}
