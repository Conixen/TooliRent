using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TooliRent.Core.Interfaces.IRepository;
using TooliRent.Core.Interfaces.IService;
using TooliRent.DTO_s.OrderDetailsDTOs;
using TooliRent.Models;

namespace TooliRent.Services.Service
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly IOrderDetailsRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IToolRepository _toolRepository;
        public OrderDetailsService(IOrderDetailsRepository orderDetailsRepository, IMapper mapper, IToolRepository toolRepository)
        {
            _orderRepository = orderDetailsRepository;
            _mapper = mapper;
            _toolRepository = toolRepository;
        }
        public async Task<OrderSummaryDTO?> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                return null;

            return _mapper.Map<OrderSummaryDTO>(order);
        }

        public async Task<List<OrderSummaryDTO>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<List<OrderSummaryDTO>>(orders);
        }

        public async Task<List<OrderSummaryDTO>> GetByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetByUserIdAsync(userId);
            return _mapper.Map<List<OrderSummaryDTO>>(orders);
        }

        public async Task<OrderSummaryDTO> CreateAsync(CreateOrderDTO dto, int userId)
        {
            // Validate dates
            if (dto.Date2Return <= dto.Date2Hire)
                throw new ArgumentException("Return date must be after hire date");

            if (dto.Date2Hire < DateTime.UtcNow.Date)
                throw new ArgumentException("Hire date cannot be in the past");

            // Get tool to calculate price
            var tool = await _toolRepository.GetByIdAsync(dto.ToolId);
            if (tool == null)
                throw new ArgumentException("Tool not found");

            if (!tool.IsAvailable)
                throw new InvalidOperationException("Tool is not available");

            // Create order
            var order = _mapper.Map<OrderDeatils>(dto);
            order.UserId = userId;
            order.CreatedAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;

            // Calculate total price
            var rentalDays = (dto.Date2Return - dto.Date2Hire).Days + 1;
            order.TotalPrice = tool.PricePerDay * rentalDays;

            var createdOrder = await _orderRepository.AddAsync(order);

            // Get complete order with relationships
            var fullOrder = await _orderRepository.GetByIdAsync(createdOrder.Id);
            return _mapper.Map<OrderSummaryDTO>(fullOrder);
        }

        public async Task UpdateAsync(int id, UpdateOrderDTO dto)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(id);

            if (existingOrder == null)
                throw new ArgumentException("Order not found");

            if (existingOrder.Status == "CheckedOut" || existingOrder.Status == "Returned")
                throw new InvalidOperationException("Cannot modify order that has been checked out or returned");

            // Validate dates
            if (dto.Date2Return <= dto.Date2Hire)
                throw new ArgumentException("Return date must be after hire date");

            // Get tool to recalculate price
            var tool = await _toolRepository.GetByIdAsync(dto.ToolId);
            if (tool == null)
                throw new ArgumentException("Tool not found");

            // Update order
            _mapper.Map(dto, existingOrder);
            existingOrder.UpdatedAt = DateTime.UtcNow;

            // Recalculate total price
            var rentalDays = (dto.Date2Return - dto.Date2Hire).Days + 1;
            existingOrder.TotalPrice = tool.PricePerDay * rentalDays;

            await _orderRepository.UpdateAsync(existingOrder);
        }

        public async Task CheckOutAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                throw new ArgumentException("Order not found");

            if (order.Status != "Pending")
                throw new InvalidOperationException("Only pending orders can be checked out");

            order.Status = "CheckedOut";
            order.CheckedOutAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order);
        }

        public async Task ReturnAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                throw new ArgumentException("Order not found");

            if (order.Status != "CheckedOut")
                throw new InvalidOperationException("Only checked out orders can be returned");

            order.Status = "Returned";
            order.ReturnedAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;

            // Calculate late fee if overdue
            if (DateTime.UtcNow > order.Date2Return)
            {
                order.LateFee = await CalculateLateFeeAsync(id);
            }

            await _orderRepository.UpdateAsync(order);
        }

        public async Task CancelAsync(int id, OrderCancelDTO dto)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                throw new ArgumentException("Order not found");

            if (order.Status != "Pending")
                throw new InvalidOperationException("Only pending orders can be cancelled");

            order.Status = "Cancelled";
            order.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order);
        }

        public async Task<List<OrderSummaryDTO>> GetActiveOrdersAsync()
        {
            var orders = await _orderRepository.GetActiveOrdersAsync();
            return _mapper.Map<List<OrderSummaryDTO>>(orders);
        }

        public async Task<List<OrderSummaryDTO>> GetOverdueOrdersAsync()
        {
            var orders = await _orderRepository.GetOverdueOrdersAsync();
            return _mapper.Map<List<OrderSummaryDTO>>(orders);
        }

        public async Task<decimal> CalculateLateFeeAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
                throw new ArgumentException("Order not found");

            if (DateTime.UtcNow <= order.Date2Return)
                return 0;

            var daysLate = (DateTime.UtcNow - order.Date2Return).Days;
            var tool = await _toolRepository.GetByIdAsync(order.ToolId);

            // Late fee: 50% of daily price per day late
            return tool.PricePerDay * 0.5m * daysLate;
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                throw new ArgumentException("Order not found");

            await _orderRepository.DeleteAsync(id);
        }
    }
}
