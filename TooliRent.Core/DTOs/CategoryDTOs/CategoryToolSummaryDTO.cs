namespace TooliRent.DTO_s.CategoryDTOs
{
    public class CategoryToolSummaryDTO // admin snd user
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
    }
}
