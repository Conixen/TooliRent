namespace TooliRent.DTO_s.ToolsDTOs
{
    public class ToolDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public string? SerialNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ToolCategoryDTO Category { get; set; } = null!;
    }
}
