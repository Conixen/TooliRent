using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.DTOs.AuthDTOs;
using TooliRent.Models;

namespace TooliRent.Core.Interfaces.IRepository
{
    public interface IUserRepository    
    {
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User> CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);

        Task<User> GetByEmailAsync(string email);   // get user by email
        Task<bool> EmailExistsAsync(string email);  // check if email exists
    }
}
