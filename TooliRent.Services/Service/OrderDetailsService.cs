using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TooliRent.Core.DTOs.OrderDetailsDTOs;
using TooliRent.Core.Interfaces.IRepository;
using TooliRent.Core.Interfaces.IService;
using TooliRent.DTO_s.OrderDetailsDTOs;
using TooliRent.Models;

namespace TooliRent.Services.Service
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly IOrderDetailsRepository _orderRepository;
        private readonly IToolRepository _toolRepository;
        private readonly IMapper _mapper;

        public OrderDetailsService(IOrderDetailsRepository orderRepository, IMapper mapper, IToolRepository toolRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _toolRepository = toolRepository;
        }

        public async Task<IEnumerable<OrderDetailsDTO>> GetAllAsync(CancellationToken ct = default)
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDetailsDTO>>(orders);
        }

        public async Task<OrderDetailsDTO?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return null;

            return _mapper.Map<OrderDetailsDTO>(order);
        }

        public async Task<ToolsOrdersResponseDTO> CreateAsync(CreateOrderDTO create, int userId, CancellationToken ct = default)
        {
            var createdTools = new List<OrderToolDTO>();

            foreach (var toolId in create.ToolIds)
            {
                var tool = await _toolRepository.GetByIdAsync(toolId);  // Get tool details
                if (tool == null)
                {
                    throw new KeyNotFoundException($"Tool with ID {toolId} not found");
                }

                if (!tool.IsAvailable)  // isAvailable
                {
                    throw new InvalidOperationException($"Tool '{tool.Name}' is not available");
                }

                var allOrders = await _orderRepository.GetAllAsync();
                var conflictingBooking = allOrders.FirstOrDefault(o =>          // Check for date conflicts
                    o.ToolId == toolId &&
                    (o.Status == "Pending" || o.Status == "CheckedOut") &&
                    (
                        (create.Date2Hire >= o.Date2Hire && create.Date2Hire <= o.Date2Return) ||
                        (create.Date2Return >= o.Date2Hire && create.Date2Return <= o.Date2Return) ||
                        (create.Date2Hire <= o.Date2Hire && create.Date2Return >= o.Date2Return)
                    )
                );

                if (conflictingBooking != null) // Conflict found
                {
                    throw new InvalidOperationException(
                        $"Tool '{tool.Name}' is already booked from {conflictingBooking.Date2Hire:yyyy-MM-dd} to {conflictingBooking.Date2Return:yyyy-MM-dd}"
                    );
                }

                var order = new OrderDeatils    // Create order
                {
                    ToolId = toolId,
                    UserId = userId,
                    Date2Hire = create.Date2Hire,
                    Date2Return = create.Date2Return,
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var createdOrder = await _orderRepository.CreateAsync(order);
                var fullOrder = await _orderRepository.GetByIdAsync(createdOrder.Id);

                createdTools.Add(new OrderToolDTO   // Add to response
                {
                    OrderId = fullOrder.Id,
                    ToolId = fullOrder.ToolId,
                    ToolName = fullOrder.Tool.Name
                });
            }

            var toolCount = createdTools.Count;
            var toolNames = string.Join(", ", createdTools.Select(t => t.ToolName));

            return new ToolsOrdersResponseDTO   // Return summary
            {
                Message = $"{toolCount} tool{(toolCount > 1 ? "s" : "")} booked successfully: {toolNames}",
                Date2Hire = create.Date2Hire,
                Date2Return = create.Date2Return,
                Status = "Pending",
                Tools = createdTools
            };
        }


        public async Task<OrderDetailsDTO?> UpdateAsync(int id, UpdateOrderDTO update, CancellationToken ct = default)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(id);
            if (existingOrder == null)
                return null;

            _mapper.Map(update, existingOrder);
            existingOrder.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(existingOrder);

            return _mapper.Map<OrderDetailsDTO>(existingOrder);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {id} not found.");

            await _orderRepository.DeleteAsync(id);
        }


        public async Task<OrderDetailsDTO?> CheckOutAsync(int orderId, CancellationToken ct = default)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return null;

            if (order.Status != "Pending")
                throw new InvalidOperationException("Only pending orders can be checked out");

            order.Status = "CheckedOut";
            order.CheckedOutAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order);
            return _mapper.Map<OrderDetailsDTO>(order);
        }

        public async Task<OrderDetailsDTO?> ReturnAsync(int orderId, CancellationToken ct = default)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return null;

            if (order.Status != "CheckedOut")
                throw new InvalidOperationException("Only checked out orders can be returned");

            order.Status = "Returned";
            order.ReturnedAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;

            // Beräkna förseningsavgift om för sent
            if (DateTime.UtcNow > order.Date2Return)
            {
                var daysLate = (DateTime.UtcNow - order.Date2Return).Days;
                order.LateFee = daysLate * 50m; // 50kr per dag i förseningsavgift
            }

            await _orderRepository.UpdateAsync(order);
            return _mapper.Map<OrderDetailsDTO>(order);
        }

        public async Task<OrderDetailsDTO?> CancelAsync(int orderId, CancellationToken ct = default)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return null;

            if (order.Status != "Pending")
                throw new InvalidOperationException("Only pending orders can be cancelled");

            order.Status = "Cancelled";
            order.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order);
            return _mapper.Map<OrderDetailsDTO>(order);
        }
    }
}
