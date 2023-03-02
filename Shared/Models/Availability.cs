using System.ComponentModel.DataAnnotations;

namespace SMS.Shared.Models
{
    public class Availability
    {
        [Key]
        public int Id { get; set; }
        public int FixtureId { get; set; }
        public int PlayerId { get; set; }
    }
}