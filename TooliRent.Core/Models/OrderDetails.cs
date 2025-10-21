using System.ComponentModel.DataAnnotations;

namespace TooliRent.Models
{
    public class OrderDeatils
    {
        public int Id { get; set; }
        public DateTime Date2Hire { get; set; }
        public DateTime Date2Return { get; set; }
        public string Status { get; set; } // Pending, CheckedOut, Returned, Cancelled
        public decimal TotalPrice { get; set; }
        public decimal? LateFee { get; set; }
        public DateTime? CheckedOutAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Foreign keys
        public int UserId { get; set; }
        public int ToolId { get; set; }
        public int? ReservationId { get; set; } // Optional - order kan skapas direkt utan reservation

        // Navigation properties
        public User User { get; set; }
        public Tool Tool { get; set; }
        public Reservation? Reservation { get; set; }
    }
}
