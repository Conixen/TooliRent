namespace TooliRent.DTO_s.CategoryDTOs
{
    public class CategoryDetailDTO  // admin snd user
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<CategoryToolSummaryDTO> Tools { get; set; } = new List<CategoryToolSummaryDTO>();
        public CategoryStatsDTO Stats { get; set; } = new CategoryStatsDTO();
    }
}
