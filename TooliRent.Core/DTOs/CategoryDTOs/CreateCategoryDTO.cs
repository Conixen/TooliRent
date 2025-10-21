using System.ComponentModel.DataAnnotations;

namespace TooliRent.DTO_s.CategoryDTOs
{
    public class CreateCategoryDTO  // Admin only
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
