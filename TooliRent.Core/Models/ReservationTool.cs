using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TooliRent.Models
{
    public class ReservationTool
    {
        public int Id { get; set; }
        public int ToolId { get; set; }
        public int ReservationId { get; set; }

        [Column(TypeName = "decimal(10,2)")]    // specify precision for decimal
        public decimal EstimatedPrice { get; set; }

        public int EstimatedDays { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        // foreign key
        public Tool Tool { get; set; }
        public Reservation Reservation { get; set; }
    }
}
