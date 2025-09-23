namespace TooliRent.DTOs.AuthDTOs
{
    public class AuthResponseDTO
    {
        public bool GreatSucessVeryNice { get; set; } = true; 
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
        public UserDTO? User { get; set; } = null!;
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

    }
}
