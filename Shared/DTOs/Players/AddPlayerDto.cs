using System.ComponentModel.DataAnnotations;

namespace SMS.Shared.DTOs.Players
{
    public class AddPlayerDto
    {
        // Notice no ID field in this DTO as the ID isn't data a user would submit 
        // it is purely an internal property of data more related to the internal infrastructure of our system
        // i.e. the primary key of the database.

        // Note we can still use data annotations on DTOs like we do on models
        // to add rules (data validations) to the data we receive when some request the POST endpoint
        // to add a new player to the system.

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