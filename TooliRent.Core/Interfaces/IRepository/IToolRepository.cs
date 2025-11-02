using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.DTO_s.ToolsDTOs;
using TooliRent.Models;

namespace TooliRent.Core.Interfaces.IRepository
{
    public interface IToolRepository
    {
        Task<IEnumerable<Tool>> GetAllAsync();
        Task<Tool?> GetByIdAsync(int id);
        Task<Tool> CreateAsync(Tool tool);
        Task UpdateAsync(Tool tool);
        Task DeleteAsync(int id);
        Task<IEnumerable<Tool>> GetAvailableAsync();
        Task<IEnumerable<Tool>> GetByCategoryAsync(int categoryId);

    }
}
