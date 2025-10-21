namespace TooliRent.DTO_s.OrderDetailsDTOs
{
    public class OrderToolDTO
    {
        public int Id { get; set; }
        public DateTime Date2Hire { get; set; }
        public DateTime Date2Return { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? LateFee { get; set; }
        public DateTime? CheckedOutAt { get; set; }
        public DateTime? ReturnedAt { get; set; }

        // User info
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        // Tool info
        public int ToolId { get; set; }
        public string ToolName { get; set; }
        public string ToolBrand { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

