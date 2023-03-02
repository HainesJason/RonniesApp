namespace SMS.Shared.DTOs
{
    public class AvailableToggleDTO
    {
        public int FixtureId { get; set; }
        public string Opponent { get; set; } = string.Empty;
        public string Venue { get; set; } = string.Empty;
        public string DateOfFixture { get; set; }
        public string StartTime { get; set; } = string.Empty;
        public bool CurrentAvailabilityStatus { get; set; }
    }
}