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

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDTO dto)
        {
            // Mappa DTO till entity
            var category = _mapper.Map<Category>(dto);

            // Spara i databas
            var savedCategory = await _categoryRepository.AddAsync(category);

            // Mappa tillbaka till DTO och returnera
            return _mapper.Map<CategoryDto>(savedCategory);
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(int id, UpdateCategoryDTO dto)
        {
            // Hämta befintlig category
            var existingCategory = await _categoryRepository.GetByIdAsync(id);

            // Mappa uppdateringar från DTO till entity
            _mapper.Map(dto, existingCategory);

            // Spara ändringar
            await _categoryRepository.UpdateAsync(existingCategory);

            // Returnera uppdaterad category som DTO
            return _mapper.Map<CategoryDto>(existingCategory);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public Task<Category> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Category> AddAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
