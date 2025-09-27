using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TooliRent.Core.Interfaces.IService;
using TooliRent.DTO_s.OrderDetailsDTOs;

namespace TooliRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailsService _orderDetailsService;

        public OrderDetailsController(IOrderDetailsService orderDetailsService)
        {
            _orderDetailsService = orderDetailsService;
        }

        // GET: api/orderdetails
        /// <summary>
        /// Get All Orders (Admin only)
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]  // admin only
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderDetailsService.GetAllAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/orderdetails/active
        /// <summary>
        /// Get Active Orders (Admin only)
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveOrders()
        {
            try
            {
                var orders = await _orderDetailsService.GetActiveOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/orderdetails/overdue
        /// <summary>
        /// Get Overdue Orders (Admin only)
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]    
        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdueOrders()
        {
            try
            {
                var orders = await _orderDetailsService.GetOverdueOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/orderdetails/{id}
        /// <summary>
        /// Get Order by ID (Admin only)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var order = await _orderDetailsService.GetByIdAsync(id);

                if (order == null)
                    return NotFound($"Order with id {id} not found");

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/orderdetails/user/{userId}
        /// <summary>
        /// Get Orders by User ID (Admin only)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserOrders(int userId)
        {
            try
            {
                var orders = await _orderDetailsService.GetByUserIdAsync(userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/orderdetails/user/{userId}
        /// <summary>
        /// Create Order for User (Admin and Member)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Member")]   // admin and member only
        [HttpPost("user/{userId}")]
        public async Task<IActionResult> CreateOrder(int userId, [FromBody] CreateOrderDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdOrder = await _orderDetailsService.CreateAsync(dto, userId);
                return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/orderdetails/{id}
        /// <summary>
        /// Update Order (Admin and Member)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Member")]   // admin and member only
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _orderDetailsService.UpdateAsync(id, dto);
                return Ok("Order updated successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PATCH: api/orderdetails/{id}/checkout
        /// <summary>
        /// Check Out Order (Admin and Member)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Member")]   // admin and member only
        [HttpPatch("{id}/checkout")]
        public async Task<IActionResult> CheckOutOrder(int id)
        {
            try
            {
                await _orderDetailsService.CheckOutAsync(id);
                return Ok("Order checked out successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PATCH: api/orderdetails/{id}/return
        /// <summary>
        /// Return Order (Admin and Member)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Member")]   // admin and member only
        [HttpPatch("{id}/return")]
        public async Task<IActionResult> ReturnOrder(int id)
        {
            try
            {
                await _orderDetailsService.ReturnAsync(id);
                return Ok("Order returned successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PATCH: api/orderdetails/{id}/cancel
        /// <summary>
        /// Cancel Order (Admin and Member)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Member")]   // admin and member only
        [HttpPatch("{id}/cancel")]
        public async Task<IActionResult> CancelOrder(int id, [FromBody] OrderCancelDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _orderDetailsService.CancelAsync(id, dto);
                return Ok("Order cancelled successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/orderdetails/{id}/late-fee
        /// <summary>
        /// Calculate Late Fee for Order (Admin and Member)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Member")]   // admin and member only
        [HttpGet("{id}/late-fee")]
        public async Task<IActionResult> CalculateLateFee(int id)
        {
            try
            {
                var lateFee = await _orderDetailsService.CalculateLateFeeAsync(id);
                return Ok(new { OrderId = id, LateFee = lateFee });
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

        // DELETE: api/orderdetails/{id}
        /// <summary>
        /// Delete Order (Admin only)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                await _orderDetailsService.DeleteAsync(id);
                return Ok("Order deleted successfully");
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
