namespace TooliRent.DTOs.AuthDTOs
{
    public class AuthResponseDTO
    {
        public bool GreatSucessVeryNice { get; set; } = true;
        public string Message { get; set; }
        public string? Token { get; set; }
        public UserDTO? User { get; set; }
    }
}
