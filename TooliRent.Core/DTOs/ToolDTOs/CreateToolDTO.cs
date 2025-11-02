using System.ComponentModel.DataAnnotations;

namespace TooliRent.DTO_s.ToolsDTOs
{
    public class CreateToolDTO  // admin only
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public decimal PricePerDay { get; set; }
        public string? SerialNumber { get; set; }
        public int CategoryId { get; set; }

    }
}
