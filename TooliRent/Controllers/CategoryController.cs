using Microsoft.AspNetCore.Mvc;
//using TooliRent.Core.DTOs.CategoryDTOs;
using TooliRent.Core.Interfaces.IService;
//using TooliRent.Core.Interfaces.Service;
using TooliRent.DTO_s.CategoryDTOs;

namespace TooliRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/categories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                return Ok(category);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Category with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdCategory = await _categoryService.CreateCategoryAsync(dto);
                return CreatedAtAction(
                    nameof(GetCategoryById),
                    new { id = createdCategory.Id },
                    createdCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/categories/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, [FromBody] UpdateCategoryDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedCategory = await _categoryService.UpdateCategoryAsync(id, dto);
                return Ok(updatedCategory);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Category with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Category with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
