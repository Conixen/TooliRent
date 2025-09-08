namespace TooliRent.DTO_s.CategoryDTOs
{
    public class CategoryStatsDTO   // Admin only
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalTools { get; set; }
        public int AvailableTools { get; set; }
        public int ReservedTools { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
