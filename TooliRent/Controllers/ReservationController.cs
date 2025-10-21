//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using TooliRent.Core.DTOs.CommonDTOs;
//using TooliRent.Core.Interfaces.IService;
//using TooliRent.DTO_s.ReservationDTOs;

//namespace TooliRent.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ReservationController : ControllerBase
//    {
//        private readonly IReservationService _reservationService;

//        public ReservationController(IReservationService reservationService)
//        {
//            _reservationService = reservationService;
//        }

//        // GET: api/reservation
//        /// <summary>
//        /// Get All Reservations (Admin Only)
//        /// </summary>
//        /// <returns></returns>
//        [Authorize(Roles = "Admin")]   // admin only
//        [HttpGet]
//        public async Task<IActionResult> GetAllReservations()
//        {
//            try
//            {
//                var reservations = await _reservationService.GetAllAsync();
//                return Ok(reservations);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        // GET: api/reservation/active
//        /// <summary>
//        /// Get Active Reservations (Admin Only)
//        /// </summary>
//        /// <returns></returns>
//        [Authorize(Roles = "Admin")]   // admin only
//        [HttpGet("active")]
//        public async Task<IActionResult> GetActiveReservations()
//        {
//            try
//            {
//                var reservations = await _reservationService.GetActiveReservationsAsync();
//                return Ok(reservations);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        // GET: api/reservation/{id}
//        /// <summary>
//        /// Get Reservation By ID
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        [Authorize(Roles = "Admin, Member")]    // admin and member only
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetReservationById(int id)
//        {
//            try
//            {
//                var reservation = await _reservationService.GetByIdAsync(id);

//                if (reservation == null)
//                    return NotFound($"Reservation with id {id} not found");

//                return Ok(reservation);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        // GET: api/reservation/user/{userId}
//        /// <summary>
//        /// Get Reservations By User ID
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        [Authorize(Roles = "Admin, Member")]    // admin and member only
//        [HttpGet("user/{userId}")]
//        public async Task<IActionResult> GetUserReservations(int userId)
//        {
//            try
//            {
//                var reservations = await _reservationService.GetByUserIdAsync(userId);
//                return Ok(reservations);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        // POST: api/reservation/user/{userId}
//        /// <summary>
//        /// Create Reservation For User
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="dto"></param>
//        /// <returns></returns>
//        [Authorize(Roles = "Admin, Member")]    // admin and member only
//        [HttpPost("user/{userId}")]
//        public async Task<IActionResult> CreateReservation(int userId, [FromBody] CreateReservationDTO dto)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            try
//            {
//                var createdReservation = await _reservationService.CreateAsync(dto, userId);
//                return CreatedAtAction(nameof(GetReservationById), new { id = createdReservation.Id }, createdReservation);
//            }
//            catch (ArgumentException ex)
//            {
//                return BadRequest(ex.Message);
//            }
//            catch (InvalidOperationException ex)
//            {
//                return Conflict(ex.Message);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        // PUT: api/reservation/{id}
//        /// <summary>
//        /// Update Reservation By ID
//        /// </summary>
//        /// <param name="id"></param>
//        /// <param name="dto"></param>
//        /// <returns></returns>
//        [Authorize(Roles = "Admin, Member")]    // admin and member only
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateReservation(int id, [FromBody] UpdateReservationDTO dto)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            try
//            {
//                await _reservationService.UpdateAsync(id, dto);
//                return Ok("Reservation updated successfully");
//            }
//            catch (ArgumentException ex)
//            {
//                return NotFound(ex.Message);
//            }
//            catch (InvalidOperationException ex)
//            {
//                return BadRequest(ex.Message);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        // PATCH: api/reservation/{id}/cancel
//        /// <summary>
//        /// Cancel Reservation By ID
//        /// </summary>
//        /// <param name="id"></param>
//        /// <param name="dto"></param>
//        /// <returns></returns>
//        [Authorize(Roles = "Admin, Member")]    // admin and member only
//        [HttpPatch("{id}/cancel")]
//        public async Task<IActionResult> CancelReservation(int id, [FromBody] ReservationCancelDTO dto)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            try
//            {
//                await _reservationService.CancelAsync(id, dto);
//                return Ok("Reservation cancelled successfully");
//            }
//            catch (ArgumentException ex)
//            {
//                return NotFound(ex.Message);
//            }
//            catch (InvalidOperationException ex)
//            {
//                return BadRequest(ex.Message);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        // GET: api/reservation/{reservationId}/can-cancel/user/{userId}
//        /// <summary>
//        /// Check If User Can Cancel Reservation
//        /// </summary>
//        /// <param name="reservationId"></param>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        [Authorize(Roles = "Admin, Member")]    // admin and member only
//        [HttpGet("{reservationId}/can-cancel/user/{userId}")]
//        public async Task<IActionResult> CanUserCancel(int reservationId, int userId)
//        {
//            try
//            {
//                var canCancel = await _reservationService.CanUserCancelAsync(reservationId, userId);
//                return Ok(new { CanCancel = canCancel });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        // GET: api/reservation/{reservationId}/can-modify/user/{userId}
//        /// <summary>
//        /// Check If User Can Modify Reservation
//        /// </summary>
//        /// <param name="reservationId"></param>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        [Authorize(Roles = "Admin, Member")]    // admin and member only
//        [HttpGet("{reservationId}/can-modify/user/{userId}")]
//        public async Task<IActionResult> CanUserModify(int reservationId, int userId)
//        {
//            try
//            {
//                var canModify = await _reservationService.CanUserModifyAsync(reservationId, userId);
//                return Ok(new { CanModify = canModify });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        // POST: api/reservation/check-availability
//        /// <summary>
//        /// Check Tool Availability
//        /// </summary>
//        /// <param name="dto"></param>
//        /// <returns></returns>
//        [Authorize]     // any authenticated user
//        [HttpPost("check-availability")]
//        public async Task<IActionResult> CheckToolAvailability([FromBody] AvailabilityCheckDTO dto)
//        {
//            try
//            {
//                var isAvailable = await _reservationService.AreToolsAvailableAsync(
//                    dto.ToolIds,
//                    dto.StartDate,
//                    dto.EndDate,
//                    dto.ExcludeReservationId
//                );

//                return Ok(new { IsAvailable = isAvailable });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        // DELETE: api/reservation/{id}
//        /// <summary>
//        /// Delete Reservation By ID
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        [Authorize(Roles = "Admin, Member")]    // admin and member only
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteReservation(int id)
//        {
//            try
//            {
//                await _reservationService.DeleteAsync(id);
//                return Ok("Reservation deleted successfully");
//            }
//            catch (ArgumentException ex)
//            {
//                return NotFound(ex.Message);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }
//    }
//}
