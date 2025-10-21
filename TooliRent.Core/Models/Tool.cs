using System.ComponentModel.DataAnnotations;

namespace TooliRent.Models
{
    public class Tool
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public string? SerialNumber { get; set; }

        // Foreign key
        public int CategoryId { get; set; }

        // Navigation properties
        public Category Category { get; set; }
        public ICollection<OrderDeatils> OrderDetails { get; set; } = new List<OrderDeatils>();

        //public ICollection<ReservationTool> ReservationTools { get; set; } = new List<ReservationTool>();
    }

}
