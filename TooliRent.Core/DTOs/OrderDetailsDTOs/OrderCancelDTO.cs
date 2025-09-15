using System.ComponentModel.DataAnnotations;

namespace TooliRent.DTO_s.OrderDetailsDTOs
{
    public class OrderCancelDTO
    {
        [Required(ErrorMessage = "Cancellation reason is required")]
        [MaxLength(250, ErrorMessage = "Reason cannot exceed 250 characters")]
        [MinLength(5, ErrorMessage = "Please provide a reason (minimum 5 characters)")]
        public string Reason { get; set; } = string.Empty;
    }
}
