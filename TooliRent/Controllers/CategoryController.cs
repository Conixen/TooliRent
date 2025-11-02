using Microsoft.AspNetCore.Mvc;
using TooliRent.Core.Interfaces.IService;
using TooliRent.DTO_s.CategoryDTOs;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;

namespace TooliRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<CreateCategoryDTO> _createValidator;    // actuall validator
        private readonly IValidator<UpdateCategoryDTO> _updateValidator;
        public CategoriesController(ICategoryService categoryService, IValidator<CreateCategoryDTO> createValidator, IValidator<UpdateCategoryDTO> updateValidator)
        {
            _categoryService = categoryService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        // GET: api/categories
        // all can access
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories(CancellationToken ct)
        {
            var categories = await _categoryService.GetAllAsync(ct);
            return Ok(categories);
        }

        // GET: api/categories/{id}
        // all can access
        /// <summary>
        /// Get category by ID
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <param name="ct"></param>
        /// <returns>Category details</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id, CancellationToken ct)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id, ct);
                if (category == null)
                {
                    return NotFound($"Category with ID {id} not found");
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/categories
        /// <summary>
        /// Create new category (Admin only)
        /// </summary>
        /// <param name="createDTO"></param>
        /// <param name="ct"></param>
        /// <returns>Created category</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]    // admin only
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory([FromBody] CreateCategoryDTO createDTO, CancellationToken ct)
        {
            var validResult = await _createValidator.ValidateAsync(createDTO, ct);
            if (!validResult.IsValid)
            {
                return BadRequest(validResult.Errors);
            }

            try
            {
                var newCategory = await _categoryService.CreateAsync(createDTO, ct);
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = newCategory.Id },
                    newCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // PUT: api/categories/{id}
        /// <summary>
        /// Update category (Admin only)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDTO"></param>
        /// <param name="ct"></param>
        /// <returns>Updated category</returns>
        [Authorize(Roles = "Admin")]    // admin only
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory(int id, [FromBody] UpdateCategoryDTO updateDTO, CancellationToken ct)
        {
            var validResult = await _updateValidator.ValidateAsync(updateDTO, ct);
            if (!validResult.IsValid)
            {
                return BadRequest(validResult.Errors);
            }

            try
            {
                var updatedCategory = await _categoryService.UpdateAsync(id, updateDTO, ct);
                if (updatedCategory == null)
                {
                    return NotFound($"Category with ID {id} not found");
                }
                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/categories/{id}
        /// <summary>
        /// Delete category (Admin only)
        /// </summary>
        /// <param name="id">Category ID to delete
        /// <param name="ct"></param></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]    // admin only
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCategory(int id, CancellationToken ct)
        {
            try
            {
                await _categoryService.DeleteAsync(id, ct);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
