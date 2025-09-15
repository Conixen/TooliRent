namespace TooliRent.DTO_s.ReservationDTOs
{
    public class ReservationSummaryDTO  // admin only
    {
        public int Id { get; set; }
        public DateTime Date2Hire { get; set; }
        public DateTime Date2Return { get; set; }
        public string Status { get; set; } = string.Empty;
        public int TotalTools { get; set; }
        public decimal EstimatedTotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
