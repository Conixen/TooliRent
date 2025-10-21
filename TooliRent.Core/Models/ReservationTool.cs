using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TooliRent.Models
{
    public class ReservationTool
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int ToolId { get; set; }
        public decimal EstimatedPrice { get; set; }
        public int EstimatedDays { get; set; }
        public string? Notes { get; set; }

        // Navigation properties
        public Reservation Reservation { get; set; }
        public Tool Tool { get; set; }
    }
}
