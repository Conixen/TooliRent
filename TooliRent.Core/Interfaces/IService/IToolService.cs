using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.DTO_s.ToolsDTOs;
using TooliRent.Models;

namespace TooliRent.Core.Interfaces.IService
{
    public interface IToolService
    {
        Task<ToolDetailDTO?> GetByIdAsync(int id);
        Task<List<ToolSummaryDTO>> GetAllAsync();
        Task<List<ToolSummaryDTO>> GetAvailableAsync();
        Task<List<ToolSummaryDTO>> GetByCategoryAsync(int categoryId);
        Task<ToolDetailDTO> CreateAsync(CreateToolDto dto);
        Task UpdateAsync(int id, UpdateToolDTO dto);
        Task UpdateStatusAsync(int id, UpdateToolStatusDTO dto);
        Task DeleteAsync(int id);
        Task<List<ToolSelectDTO>> GetForSelectAsync();
    }
}