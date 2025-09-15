using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.DTOs.AuthDTOs;
using TooliRent.Models;

namespace TooliRent.Core.Interfaces.IService
{
    public interface IAuthService  
    {
        // Authentication
        Task<AuthResponseDTO> RegisterAsync(CreateUserDTO dto);
        Task<AuthResponseDTO> LoginAsync(LogInDTO dto);

        // Profile management
        Task<UserDTO> GetProfileAsync(int userId);
        Task UpdateProfileAsync(int userId, UpdateUserDTO dto);

        // Password management
        Task ChangePasswordAsync(int userId, ForgotPasswordDTO dto);
        Task ForgotPasswordAsync(ForgotPasswordDTO dto);

        // Admin operations (om du har UpdateUserStatusDto)
        Task UpdateUserStatusAsync(int userId, UpdateUserStatusDTO dto);
    }
}
