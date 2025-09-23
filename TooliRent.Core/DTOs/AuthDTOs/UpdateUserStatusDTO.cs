using System.ComponentModel.DataAnnotations;

namespace TooliRent.DTOs.AuthDTOs
{
    public class UpdateUserStatusDTO
    {
        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = string.Empty; // Admin, Member
        
    }
}
