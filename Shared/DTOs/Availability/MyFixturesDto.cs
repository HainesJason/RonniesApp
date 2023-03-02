using System;

namespace SMS.Shared.DTOs.Availability
{
    public class MyFixturesDto
    {
        public int FixtureId { get; set; }
        public string FixtureDetail { get; set; } = string.Empty;
        public DateTime DateOfFixture { get; set; }
    }
}