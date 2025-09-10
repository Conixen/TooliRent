using System.ComponentModel.DataAnnotations;

namespace TooliRent.DTO_s.ToolsDTOs
{
    public class CreateToolDto  // admin only
    {
        [Required(ErrorMessage = "Tool name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Brand is required")]
        [MaxLength(50, ErrorMessage = "Brand cannot exceed 50 characters")]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required")]
        [MaxLength(50, ErrorMessage = "Model cannot exceed 50 characters")]
        public string Model { get; set; } = string.Empty;
        
        [MaxLength(100, ErrorMessage = "Serial number cannot exceed 100 characters")]
        public string? SerialNumber { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price per day is required")]
        [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10000")]
        public decimal PricePerDay { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category")]
        public int CategoryId { get; set; }

    }
}
