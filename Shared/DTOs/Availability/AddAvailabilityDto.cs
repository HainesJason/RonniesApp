using System.ComponentModel.DataAnnotations;

namespace SMS.Shared.DTOs.Availability
{
    public class AddAvailabilityDto
    {
        // Based on the assumption each fixture data entry by the user triggers a request to the API

        [Required]
        public int PlayerId { get; set; }
        [Required]
        public int FixtureId { get; set; }

        public bool IsAvailable { get; set; }
    }
}