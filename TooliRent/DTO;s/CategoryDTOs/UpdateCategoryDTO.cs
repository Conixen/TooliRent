using System.ComponentModel.DataAnnotations;

namespace TooliRent.DTO_s.CategoryDTOs
{
    public class UpdateCategoryDTO  // Admin only
    {
        [Required(ErrorMessage = "Category name is required")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
        [MinLength(10, ErrorMessage = "Description must be at least 10 characters")]
        public string Description { get; set; } = string.Empty;
    }
}
