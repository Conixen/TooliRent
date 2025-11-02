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
        Task<IEnumerable<ToolDTO>> GetAllAsync(CancellationToken ct = default);
        Task<ToolDTO?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<ToolDTO> CreateAsync(CreateToolDTO create, CancellationToken ct = default);
        Task<ToolDTO?> UpdateAsync(int id, UpdateToolDTO update, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
        Task<IEnumerable<ToolDTO>> GetAvailableAsync(CancellationToken ct = default);
        Task<IEnumerable<ToolDTO>> GetByCategoryAsync(int categoryId, CancellationToken ct = default);
    }
}