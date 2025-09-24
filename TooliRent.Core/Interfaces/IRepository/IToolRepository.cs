using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.Models;

namespace TooliRent.Core.Interfaces.IRepository
{
    public interface IToolRepository
    {
        Task<Tool?> GetByIdAsync(int id);
        Task<List<Tool>> GetAllAsync();
        Task<List<Tool>> GetAvailableAsync();
        Task<List<Tool>> GetByCategoryAsync(int categoryId);
        Task<Tool> AddAsync(Tool tool);
        Task UpdateAsync(Tool tool);
        Task DeleteAsync(int id);

    }
}
