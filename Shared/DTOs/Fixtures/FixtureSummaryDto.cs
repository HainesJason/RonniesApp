namespace SMS.Shared.DTOs.Fixtures
{
    public class FixtureSummaryDto
    {
        public int Id { get; set; }
        public string Opponent { get; set; } = string.Empty;
        public string DateOfFixture { get; set; }
        //public string Venue { get; set; } = string.Empty;
        public string Venue { get; set; }
        public string StartTime { get; set; } = string.Empty;
        public int NumberOfPlayersRequired { get; set; }
    }
}