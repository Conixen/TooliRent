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
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<List<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDTO dto);
        Task<CategoryDto> UpdateCategoryAsync(int id, UpdateCategoryDTO dto);
        Task DeleteCategoryAsync(int id);
    }

}
