using System.ComponentModel.DataAnnotations;

namespace TooliRent.DTOs.AuthDTOs
{
    public class LogInDTO
    {

        public string Email { get; set; } = string.Empty;


        public string Password { get; set; } = string.Empty;
    }
}
