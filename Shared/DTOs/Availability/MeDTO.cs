using System.Collections.Generic;

namespace SMS.Shared.DTOs.Availability
{
    public class MeDTO
    {
        public int PlayerId { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public List<AvailableToggleDTO> PlayersCurrentAvailability { get; set; } = new List<AvailableToggleDTO>();
    }
}