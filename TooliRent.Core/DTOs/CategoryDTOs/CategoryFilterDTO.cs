using System.ComponentModel.DataAnnotations;

namespace TooliRent.DTO_s.CategoryDTOs
{
    public class CategoryFilterDTO  // admin and user
    {
        [MaxLength(50)]
        public string? SearchTerm { get; set; } 

        public bool? HasTools { get; set; } 

        public bool? HasAvailableTools { get; set; } 

        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100)]
        public int PageSize { get; set; } = 10;

        public string SortBy { get; set; } = "Name"; 

        public string SortDirection { get; set; } = "ASC"; 
    }
}
