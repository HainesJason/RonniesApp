using System.ComponentModel.DataAnnotations;

namespace SMS.Shared.Models
{
    public class Player
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Only 10 characters for the firstname!!!!")]
        public string Firstname { get; set; } = string.Empty;
        [Required]
        public string Lastname { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public bool IsActivePlayer { get; set; } = true;
    }
}