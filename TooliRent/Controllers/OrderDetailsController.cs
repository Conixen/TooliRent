using FluentValidation;
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
        private readonly IOrderDetailsService _orderService;
        private readonly IValidator<CreateOrderDTO> _createValidator;
        private readonly IValidator<UpdateOrderDTO> _updateValidator;

        public OrderDetailsController(
            IOrderDetailsService orderService,
            IValidator<CreateOrderDTO> createValidator,
            IValidator<UpdateOrderDTO> updateValidator)
        {
            _orderService = orderService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        // GET: api/orderdetails
        /// <summary>
        /// Get all orders
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderDetailsDTO>>> GetAllOrders(CancellationToken ct)
        {
            var orders = await _orderService.GetAllAsync(ct);
            return Ok(orders);
        }

        // GET: api/orderdetails/{id}
        /// <summary>
        /// Get order by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDetailsDTO>> GetById(int id, CancellationToken ct)
        {
            try
            {
                var order = await _orderService.GetByIdAsync(id, ct);
                if (order == null)
                {
                    return NotFound($"Order with ID {id} not found");
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/orderdetails
        /// <summary>
        /// Create a new order
        /// </summary>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDetailsDTO>> CreateOrder([FromBody] CreateOrderDTO createDTO, CancellationToken ct)
        {
            var validResult = await _createValidator.ValidateAsync(createDTO, ct);
            if (!validResult.IsValid)
            {
                return BadRequest(validResult.Errors);
            }

            try
            {
                var newOrder = await _orderService.CreateAsync(createDTO, ct);
                return Ok(newOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/orderdetails/{id}
        /// <summary>
        /// Update an order
        /// </summary>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDetailsDTO>> UpdateOrder(int id, [FromBody] UpdateOrderDTO updateDTO, CancellationToken ct)
        {
            var validResult = await _updateValidator.ValidateAsync(updateDTO, ct);
            if (!validResult.IsValid)
            {
                return BadRequest(validResult.Errors);
            }

            try
            {
                var updatedOrder = await _orderService.UpdateAsync(id, updateDTO, ct);
                if (updatedOrder == null)
                {
                    return NotFound($"Order with ID {id} not found");
                }
                return Ok(updatedOrder);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/orderdetails/{id}
        /// <summary>
        /// Delete an order (Admin only)
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteOrder(int id, CancellationToken ct)
        {
            try
            {
                await _orderService.DeleteAsync(id, ct);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        // GET: api/orderdetails/overdue
        /// <summary>
        /// Get Overdue Orders (Admin only)
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = "Admin")]    
        //[HttpGet("overdue")]
        //public async Task<IActionResult> GetOverdueOrders()
        //{
        //    try
        //    {
        //        var orders = await _orderDetailsService.GetOverdueOrdersAsync();
        //        return Ok(orders);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        // GET: api/orderdetails/user/{userId}
        /// <summary>
        /// Get Orders by User ID (Admin only)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //[Authorize(Roles = "Admin")]
        //[HttpGet("user/{userId}")]
        //public async Task<IActionResult> GetUserOrders(int userId)
        //{
        //    try
        //    {
        //        var orders = await _orderDetailsService.GetByUserIdAsync(userId);
        //        return Ok(orders);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}


        //// PATCH: api/orderdetails/{id}/cancel
        ///// <summary>
        ///// Cancel Order (Admin and Member)
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[Authorize(Roles = "Admin, Member")]   // admin and member only
        //[HttpPatch("{id}/cancel")]
        //public async Task<IActionResult> CancelOrder(int id, [FromBody] OrderCancelDTO dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    try
        //    {
        //        await _orderDetailsService.CancelAsync(id, dto);
        //        return Ok("Order cancelled successfully");
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        //// GET: api/orderdetails/{id}/late-fee
        ///// <summary>
        ///// Calculate Late Fee for Order (Admin and Member)
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[Authorize(Roles = "Admin, Member")]   // admin and member only
        //[HttpGet("{id}/late-fee")]
        //public async Task<IActionResult> CalculateLateFee(int id)
        //{
        //    try
        //    {
        //        var lateFee = await _orderDetailsService.CalculateLateFeeAsync(id);
        //        return Ok(new { OrderId = id, LateFee = lateFee });
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
        //// POST: api/orderdetails/user/{userId}
        ///// <summary>
        ///// Create Order for User (Admin and Member)
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[Authorize(Roles = "Admin, Member")]   // admin and member only
        //[HttpPost("user/{userId}")]
        //public async Task<IActionResult> CreateOrder(int userId, [FromBody] CreateOrderDTO dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    try
        //    {
        //        var createdOrder = await _orderDetailsService.CreateAsync(dto, userId);
        //        return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return Conflict(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}


    }
}
