using System.ComponentModel.DataAnnotations;

namespace TooliRent.DTO_s.CategoryDTOs
{
    public class ToolDTO  // Admin only
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public string? SerialNumber { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
