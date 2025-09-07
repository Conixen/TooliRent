using TooliRent.DTO_s.ToolsDTOs;

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


        public ReservationUserDTO User { get; set; } = null!;
        public List<ToolSummaryDTO> Tools { get; set; } = new List<ToolSummaryDTO>();
        public int TotalTools { get; set; }
        public decimal EstimatedTotalPrice { get; set; }
        public int DurationDays { get; set; }
        public bool CanBeCancelled { get; set; }
        public bool CanBeModified { get; set; }
    }
}
