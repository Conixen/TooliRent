using System.ComponentModel.DataAnnotations;

namespace TooliRent.Models
{
    public class Reservation
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Date2Hire { get; set; }

        [Required]
        public DateTime Date2Return { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } // available, reserved, not available

        public DateTime CanceledAt { get; set; }
        [MaxLength(250)]
        public string? CanceledReason { get; set; }


        // foreign key
        public int UserId { get; set; }
        //public int ToolId { get; set; }

        // navigation property
        public User User { get; set; }
        public ICollection<ReservationTool> ReservationTools { get; set; } = new List<ReservationTool>();
        public ICollection<OrderDeatils> OrderDetails { get; set; } = new List<OrderDeatils>();

    }
}
