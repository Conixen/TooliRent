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

        public OrderDetailsService(IOrderDetailsRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
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

        public async Task<OrderDetailsDTO> CreateAsync(CreateOrderDTO create, CancellationToken ct = default)
        {
            var order = _mapper.Map<OrderDeatils>(create);

            order.Status = "Pending";   // Default status
            order.CreatedAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;

            var createdOrder = await _orderRepository.CreateAsync(order);
            return _mapper.Map<OrderDetailsDTO>(createdOrder);
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
