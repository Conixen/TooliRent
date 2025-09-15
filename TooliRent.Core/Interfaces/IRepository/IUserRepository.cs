using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.Models;

namespace TooliRent.Core.Interfaces.IRepository
{
    public interface IUserRepository    // För AuthService
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);  
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<bool> EmailExistsAsync(string email); 
    }
}
