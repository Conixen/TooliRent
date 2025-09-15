using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.DTO_s.CategoryDTOs;

namespace TooliRent.Services.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDTO dto);
        Task<List<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(int id, UpdateCategoryDTO dto);
        Task DeleteCategoryAsync(int id);
    }
}
