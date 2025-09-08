using System.ComponentModel.DataAnnotations;

namespace TooliRent.DTO_s.ToolsDTOs
{
    public class UpdateToolStatusDTO    // admin only
    {
        [Required(ErrorMessage = "Status is required")]
        [MaxLength(20)]
        public string Status { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}
