using System.ComponentModel.DataAnnotations;

namespace TooliRent.DTOs.AuthDTOs
{
    public class UpdateUserDTO
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(75)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
    }
}
