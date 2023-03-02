using SMS.Shared.Enums;
using System.Collections.Generic;

namespace SMS.Shared.DTOs.Availability
{
    public class PlayersAvailableForFixtureDto
    {
        public int FixtureId { get; set; }
        public string Opponents { get; set; } = string.Empty;
        public string DateOfFixture { get; set; }
        public VenueEnum Venue { get; set; }
        public string StartTime { get; set; } = string.Empty;
        public List<PlayersAvailableDto> AvailablePlayers { get; set; } = new List<PlayersAvailableDto>();

    }
}