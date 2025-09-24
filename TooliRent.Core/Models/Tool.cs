using System.ComponentModel.DataAnnotations;

namespace TooliRent.Models
{
    public class Tool
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; }  // available, reserved, not available
        
        [MaxLength(500)]
        public string Description { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }

        [MaxLength(100)]  
        public string? SerialNumber { get; set; }

        // foreign key
        public int CategoryId { get; set; }

        // navigation property
        public Category Category { get; set; }  // one tool belongs to one category
        public ICollection<ReservationTool> ReservationTools { get; set; } = new List<ReservationTool>(); // one tool can be in many reservations
        public ICollection<OrderDeatils> OrderDetails { get; set; } = new List<OrderDeatils>();     // one tool can be in many ordersdetails
    }

}
