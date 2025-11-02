using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TooliRent.Core.Interfaces.IService;
using TooliRent.DTOs.AuthDTOs;
using TooliRent.Services.Service;

namespace TooliRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidator<CreateUserDTO> _createValidator;
        private readonly IValidator<UpdateUserDTO> _updateValidator;
        private readonly IValidator<LogInDTO> _loginValidator;

        public AuthController(IAuthService authService, IValidator<CreateUserDTO> createValidator, IValidator<UpdateUserDTO> updateValidator, IValidator<LogInDTO> loginValidator)
        {
            _authService = authService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _loginValidator = loginValidator;
        }
        // GET: api/auth/users (Admin only)
        /// <summary>
        /// Get all users (Admin only)
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers(CancellationToken ct)
        {
            var users = await _authService.GetAllUsersAsync(ct);
            return Ok(users);
        }

        // GET: api/auth/profile
        /// <summary>
        /// Get current user profile
        /// </summary>
        [Authorize]
        [HttpGet("profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> GetById(CancellationToken ct)
        {
            try
            {
                // Get userId from JWT token claims
                var userIdClaim = User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Unauthorized("Invalid token");
                }

                var userId = int.Parse(userIdClaim);
                var user = await _authService.GetUserByIdAsync(userId, ct);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/auth/profile
        /// <summary>
        /// Update current user profile
        /// </summary>
        [Authorize]
        [HttpPut("profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> UpdateProfile([FromBody] UpdateUserDTO updateDTO, CancellationToken ct)
        {
            var validResult = await _updateValidator.ValidateAsync(updateDTO, ct);
            if (!validResult.IsValid)
            {
                return BadRequest(validResult.Errors);
            }

            try
            {
                var userIdClaim = User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Unauthorized("Invalid token");
                }

                var userId = int.Parse(userIdClaim);
                var updatedUser = await _authService.UpdateUserAsync(userId, updateDTO, ct);

                if (updatedUser == null)
                {
                    return NotFound("User not found");
                }

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/auth/register
        /// <summary>
        /// Register a new user
        /// </summary>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthResponseDTO>> Register([FromBody] CreateUserDTO createDTO, CancellationToken ct)
        {
            var validResult = await _createValidator.ValidateAsync(createDTO, ct);
            if (!validResult.IsValid)
            {
                return BadRequest(validResult.Errors);
            }

            try
            {
                var result = await _authService.RegisterAsync(createDTO, ct);

                if (!result.GreatSucessVeryNice)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/auth/login
        /// <summary>
        /// Login user
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] LogInDTO loginDTO, CancellationToken ct)
        {
            var validResult = await _loginValidator.ValidateAsync(loginDTO, ct);
            if (!validResult.IsValid)
            {
                return BadRequest(validResult.Errors);
            }

            try
            {
                var result = await _authService.LoginAsync(loginDTO, ct);

                if (!result.GreatSucessVeryNice)
                {
                    return Unauthorized(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        //// Post: api/auth/change-password/{userId}
        ///// <summary>
        ///// Change User Password
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[Authorize]     // any logged in user
        //[HttpPost("change-password/{userId}")]
        //public async Task<IActionResult> ChangePassword(int userId, [FromBody] ForgotPasswordDTO dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    try
        //    {
        //        await _authService.ChangePasswordAsync(userId, dto);
        //        return Ok("Password changed successfully");
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

        //// Post: api/auth/forgot-password
        ///// <summary>
        ///// Forgot Password
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[HttpPost("forgot-password")]
        //public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    try
        //    {
        //        await _authService.ForgotPasswordAsync(dto);
        //        return Ok("If the email exists, password reset instructions have been sent");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        //// Put: api/auth/admin/update-user-status/{userId}
        ///// <summary>
        ///// Admin Update User Status (Admin Only)
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[Authorize(Roles = "Admin")]
        //[HttpPut("admin/update-user-status/{userId}")]
        //public async Task<IActionResult> UpdateUserStatus(int userId, [FromBody] UpdateUserStatusDTO dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    try
        //    {
        //        await _authService.UpdateUserStatusAsync(userId, dto);
        //        return Ok("User status updated successfully");
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
