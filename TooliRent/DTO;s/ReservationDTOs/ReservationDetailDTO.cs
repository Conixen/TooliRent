namespace TooliRent.DTO_s.ReservationDTOs
{
    public class ReservationDetailDTO
    {
        public int Id { get; set; }
        public DateTime Date2Hire { get; set; }
        public DateTime Date2Return { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime? CancelledAt { get; set; }
        public string? CancellationReason { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
