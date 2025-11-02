using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TooliRent.Core.Interfaces.IRepository;
using TooliRent.Core.Interfaces.IService;
using TooliRent.DTO_s.CategoryDTOs;
using TooliRent.Models;


namespace TooliRent.Services.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync(CancellationToken ct = default)
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> CreateAsync(CreateCategoryDTO create, CancellationToken ct = default)
        {
            var category = _mapper.Map<Category>(create);
            
            var createCategory = await _categoryRepository.CreateAsync(category);
            return _mapper.Map<CategoryDTO>(createCategory);
        }

        public async Task<CategoryDTO?> UpdateAsync(int id, UpdateCategoryDTO update, CancellationToken ct = default)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }

            _mapper.Map(update, existingCategory);
            await _categoryRepository.UpdateAsync(existingCategory);

            return _mapper.Map<CategoryDTO>(existingCategory);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            await _categoryRepository.DeleteAsync(id);

        }
    }
}
