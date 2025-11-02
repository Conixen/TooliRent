using System.ComponentModel.DataAnnotations;

namespace TooliRent.DTOs.AuthDTOs
{
    public class UpdateUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
