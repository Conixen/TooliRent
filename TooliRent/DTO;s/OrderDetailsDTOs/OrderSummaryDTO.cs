namespace TooliRent.DTO_s.OrderDetailsDTOs
{
    public class OrderSummaryDTO    // Admin only
    {
        // User info (för admin-vyer)
        public string UserName { get; set; } = string.Empty;
        public int Id { get; set; }
        public DateTime Date2Hire { get; set; }
        public DateTime Date2Return { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public decimal LateFee { get; set; }
        public bool IsOverdue { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CheckedOutAt { get; set; }
        public DateTime? ReturnedAt { get; set; }

        // Tool basic info
        public string ToolName { get; set; } = string.Empty;
        public string ToolBrand { get; set; } = string.Empty;
        public string ToolModel { get; set; } = string.Empty;

    }
}
