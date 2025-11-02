using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TooliRent.Core.DTOs.StatisticksDTOs;
using TooliRent.Core.Interfaces.IService;

namespace TooliRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]    // Only Admins can access statistics
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        // GET: api/statistics
        /// <summary>
        /// Get basic rental statistics (Admin only)
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<StatisticsDTO>> GetStatistics(CancellationToken ct)
        {
            try
            {
                var statistics = await _statisticsService.GetStatisticsAsync(ct);
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
