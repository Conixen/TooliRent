using System.ComponentModel.DataAnnotations;

namespace TooliRent.DTO_s.OrderDetailsDTOs
{
    public class CreateOrderDTO // ADMIN ONLY
    {
        [Required(ErrorMessage = "Start date is required")]
        public DateTime Date2Hire { get; set; }

        [Required(ErrorMessage = "End date is required")]
        public DateTime Date2Return { get; set; }

        [Required(ErrorMessage = "Tool selection is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid tool")]
        public int ToolId { get; set; }

        [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }
    }
}
