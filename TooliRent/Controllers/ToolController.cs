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

        public ToolController(IToolService toolService)
        {
            _toolService = toolService;
        }

        // GET: api/tool
        /// <summary>
        /// Get All Tools
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllTools()
        {
            try
            {
                var tools = await _toolService.GetAllAsync();
                return Ok(tools);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/tool/available
        /// <summary>
        /// Get Available Tools 
        /// </summary>
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableTools()
        {
            try
            {
                var tools = await _toolService.GetAvailableAsync();
                return Ok(tools);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/tool/{id}
        /// <summary>
        /// Get Tool by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetToolById(int id)
        {
            try
            {
                var tool = await _toolService.GetByIdAsync(id);

                if (tool == null)
                    return NotFound($"Tool with id {id} not found");

                return Ok(tool);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/tool/category/{categoryId}
        /// <summary>
        /// Get Tools by Category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetToolsByCategory(int categoryId)
        {
            try
            {
                var tools = await _toolService.GetByCategoryAsync(categoryId);
                return Ok(tools);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/tool/select
        /// <summary>
        /// Get Tools For Select (Id and Name only)
        /// </summary>
        /// <returns></returns>
        [HttpGet("select")]
        public async Task<IActionResult> GetToolsForSelect()
        {
            try
            {
                var tools = await _toolService.GetForSelectAsync();
                return Ok(tools);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // POST: api/tool 
        /// <summary>
        /// Create A New Tool
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]    // admin only
        [HttpPost]
        public async Task<IActionResult> CreateTool([FromBody] CreateToolDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdTool = await _toolService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetToolById), new { id = createdTool.Id }, createdTool);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/tool/{id}
        /// <summary>
        /// Update Tool Details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]    // admin only
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTool(int id, [FromBody] UpdateToolDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _toolService.UpdateAsync(id, dto);
                return Ok("Tool updated successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PATCH: api/tool/{id}/status
        /// <summary>
        /// Update Tool Status (Available/Unavailable)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]    // admin only
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateToolStatus(int id, [FromBody] UpdateToolStatusDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _toolService.UpdateStatusAsync(id, dto);
                return Ok("Tool status updated successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/tool/{id}
        /// <summary>
        /// Delete A Tool
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]    // admin only
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTool(int id)
        {
            try
            {
                await _toolService.DeleteAsync(id);
                return Ok("Tool deleted successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
