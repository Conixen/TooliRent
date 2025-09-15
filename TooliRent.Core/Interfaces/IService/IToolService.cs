using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.Models;

namespace TooliRent.Core.Interfaces.IService
{
    public interface IToolService
    {
        Task<Tool> GetByIdAsync(int id);
        Task<List<Tool>> GetAllAsync();
        Task<Tool> AddAsync(Tool tool);
        Task UpdateAsync(Tool tool);
        Task DeleteAsync(int id);
    }
}
