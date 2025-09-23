using Microsoft.AspNetCore.Mvc;
using TooliRent.Core.Interfaces.IService;
using TooliRent.DTOs.AuthDTOs;

namespace TooliRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _authService.RegisterAsync(dto);

                if (result == null)
                    return BadRequest("Registration failed");

                if (!result.Success)
                    return BadRequest(result.Message);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LogInDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _authService.LoginAsync(dto);

                if (result == null)
                    return Unauthorized("Invalid credentials");

                if (!result.Success)
                    return Unauthorized(result.Message);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("profile/{userId}")]
        public async Task<IActionResult> GetProfile(int userId)
        {
            try
            {
                var user = await _authService.GetProfileAsync(userId);

                if (user == null)
                    return NotFound("User not found");

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("profile/{userId}")]
        public async Task<IActionResult> UpdateProfile(int userId, [FromBody] UpdateUserDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _authService.UpdateProfileAsync(userId, dto);
                return Ok("Profile updated successfully");
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

        [HttpPost("change-password/{userId}")]
        public async Task<IActionResult> ChangePassword(int userId, [FromBody] ForgotPasswordDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _authService.ChangePasswordAsync(userId, dto);
                return Ok("Password changed successfully");
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

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _authService.ForgotPasswordAsync(dto);
                return Ok("If the email exists, password reset instructions have been sent");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Admin endpoint
        [HttpPut("admin/update-user-status/{userId}")]
        public async Task<IActionResult> UpdateUserStatus(int userId, [FromBody] UpdateUserStatusDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _authService.UpdateUserStatusAsync(userId, dto);
                return Ok("User status updated successfully");
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
