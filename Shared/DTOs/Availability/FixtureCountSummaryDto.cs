namespace SMS.Shared.DTOs.Availability
{
    public class FixtureCountSummaryDto
    {
        public int FixtureId { get; set; }
        public string FixtureDetail { get; set; } = string.Empty;
        public string DateOfFixture { get; set; }
        public int PlayersAvailable { get; set; }
        public int PlayersRequired { get; set; }
    }
}