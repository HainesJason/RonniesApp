using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Shared.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfEvent { get; set; }
        public string StartTime { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public string Organiser { get; set; } = string.Empty;
    }
}
