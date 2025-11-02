using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.DTO_s.CategoryDTOs;
using TooliRent.Models;


namespace TooliRent.Core.Interfaces.IService
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllAsync(CancellationToken ct = default);
        Task<CategoryDTO?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<CategoryDTO> CreateAsync(CreateCategoryDTO create, CancellationToken ct = default);
        Task<CategoryDTO> UpdateAsync(int id, UpdateCategoryDTO update, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
