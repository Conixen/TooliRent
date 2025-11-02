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
        Task<AuthResponseDTO> RegisterAsync(CreateUserDTO dto, CancellationToken ct = default); // CreateUserAsync
        Task<AuthResponseDTO> LoginAsync(LogInDTO dto, CancellationToken ct = default);

        // Crud
        Task<IEnumerable<UserDTO>> GetAllUsersAsync(CancellationToken ct = default);
        Task<UserDTO?> GetUserByIdAsync(int userId, CancellationToken ct = default);
        Task<UserDTO?> UpdateUserAsync(int userId, UpdateUserDTO dto, CancellationToken ct = default);
        Task DeleteUserAsync(int userId, CancellationToken ct = default);

        Task UpdateUserStatusAsync(int userId, bool isActive, CancellationToken ct = default);  

    }
}
