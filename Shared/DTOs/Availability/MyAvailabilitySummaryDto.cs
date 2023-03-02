using System.Collections.Generic;

namespace SMS.Shared.DTOs.Availability
{
    public class MyAvailabilitySummaryDto
    {
        public int PlayerId { get; set; }
        public string Fullname { get; set; } = string.Empty;

        public List<MyFixturesDto> MyFixtures { get; set; } = new List<MyFixturesDto>();
    }
}