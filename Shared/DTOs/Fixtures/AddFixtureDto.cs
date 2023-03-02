using SMS.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace SMS.Shared.DTOs.Fixtures
{
    public class AddFixtureDto
    {
        [Required]
        [MaxLength(100)]
        public string Opponent { get; set; } = string.Empty;
        [Required]
        public string DateOfFixture { get; set; }
        [EnumDataType(typeof(VenueEnum), ErrorMessage = "Value must be 'Home' or 'Away'")]
        public VenueEnum Venue { get; set; }
        [Required]
        public string StartTime { get; set; } = string.Empty;
        public string Postcode { get; set; } = "n/a";
        [Required]
        public int NumberOfPlayersRequired { get; set; }
    }
}