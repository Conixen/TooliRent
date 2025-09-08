namespace TooliRent.DTO_s.ToolsDTOs
{
    public class ToolSelectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public string DisplayName => $"{Brand} {Model} - {Name}";
    }
}
