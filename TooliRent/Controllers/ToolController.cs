using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TooliRent.Core.Interfaces.IService;
using TooliRent.DTO_s.ToolsDTOs;

namespace TooliRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolController : ControllerBase
    {
        private readonly IToolService _toolService;
        private readonly IValidator<CreateToolDTO> _createVali;
        private readonly IValidator<UpdateToolDTO> _updateVali;

        public ToolController(IToolService toolService, IValidator<UpdateToolDTO> updateToolVali, IValidator<CreateToolDTO> createToolVali)
        {
            _toolService = toolService;
            _updateVali = updateToolVali;
            _createVali = createToolVali;
        }

        // GET: api/tools
        /// <summary>
        /// Get all tools
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ToolDTO>>> GetAllTools(CancellationToken ct)
        {
            var tools = await _toolService.GetAllAsync(ct);
            return Ok(tools);
        }

        // GET: api/tools/{id}
        /// <summary>
        /// Get tool by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ToolDTO>> GetById(int id, CancellationToken ct)
        {
            try
            {
                var tool = await _toolService.GetByIdAsync(id, ct);
                if (tool == null)
                {
                    return NotFound($"Tool with ID {id} not found");
                }
                return Ok(tool);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/tools
        /// <summary>
        /// Create a new tool (Admin only)
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ToolDTO>> CreateTool([FromBody] CreateToolDTO createDTO, CancellationToken ct)
        {
            var validResult = await _createVali.ValidateAsync(createDTO, ct);
            if (!validResult.IsValid)
            {
                return BadRequest(validResult.Errors);
            }

            try
            {
                var newTool = await _toolService.CreateAsync(createDTO, ct);
                return Ok(newTool);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/tools/{id}
        /// <summary>
        /// Update a tool (Admin only)
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ToolDTO>> UpdateTool(int id, [FromBody] UpdateToolDTO updateDTO, CancellationToken ct)
        {
            var validResult = await _updateVali.ValidateAsync(updateDTO, ct);
            if (!validResult.IsValid)
            {
                return BadRequest(validResult.Errors);
            }

            try
            {
                var updatedTool = await _toolService.UpdateAsync(id, updateDTO, ct);
                if (updatedTool == null)
                {
                    return NotFound($"Tool with ID {id} not found");
                }
                return Ok(updatedTool);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/tools/{id}
        /// <summary>
        /// Delete a tool (Admin only)
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTool(int id, CancellationToken ct)
        {
            try
            {
                await _toolService.DeleteAsync(id, ct);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        //--------------------------------------------------------------

        // GET: api/tools/available
        /// <summary>
        /// Get all available tools
        /// </summary>
        [HttpGet("available")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ToolDTO>>> GetAvailable(CancellationToken ct)
        {
            var tools = await _toolService.GetAvailableAsync(ct);
            return Ok(tools);
        }

        // GET: api/tools/category/{categoryId}
        /// <summary>
        /// Get tools by category
        /// </summary>
        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ToolDTO>>> GetByCategory(int categoryId, CancellationToken ct)
        {
            var tools = await _toolService.GetByCategoryAsync(categoryId, ct);
            return Ok(tools);
        }


        //// GET: api/tool/select
        ///// <summary>
        ///// Get Tools For Dropdown
        ///// </summary>
        //[HttpGet("select")]
        //public async Task<IActionResult> GetToolsForDropdown()
        //{
        //    try
        //    {
        //        var tools = await _toolService.GetForDropdownAsync();
        //        return Ok(tools);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        //// PATCH: api/tool/{id}/status
        ///// <summary>
        ///// Update Tool Status (Admin Only)
        ///// </summary>
        //[Authorize(Roles = "Admin")]
        //[HttpPatch("{id}/status")]
        //public async Task<IActionResult> UpdateToolStatus(int id, [FromBody] UpdateToolStatusDTO dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    try
        //    {
        //        await _toolService.UpdateStatusAsync(id, dto);
        //        return Ok("Tool status updated successfully");
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

    }
}
