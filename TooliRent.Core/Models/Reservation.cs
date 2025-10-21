//using System.ComponentModel.DataAnnotations;

//namespace TooliRent.Models
//{
//    public class Reservation
//    {
//        public int Id { get; set; }
//        public DateTime Date2Hire { get; set; }
//        public DateTime Date2Return { get; set; }
//        public string Status { get; set; } // Active, Cancelled
//        public DateTime? CanceledAt { get; set; }
//        public string? CanceledReason { get; set; }
//        public DateTime CreatedAt { get; set; }
//        public DateTime UpdatedAt { get; set; }

//        // Foreign key
//        public int UserId { get; set; }

//        // Navigation properties
//        public User User { get; set; }
//        public ICollection<ReservationTool> ReservationTools { get; set; } = new List<ReservationTool>();
//        public ICollection<OrderDeatils> OrderDetails { get; set; } = new List<OrderDeatils>();
//    }
//}
