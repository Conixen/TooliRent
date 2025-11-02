using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.Core.DTOs.StatisticksDTOs;
using TooliRent.Core.Interfaces.IRepository;
using TooliRent.Core.Interfaces.IService;

namespace TooliRent.Services.Service
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUserRepository _userRepository;
        private readonly IToolRepository _toolRepository;
        private readonly IOrderDetailsRepository _orderRepository;

        public StatisticsService(
            IUserRepository userRepository,
            IToolRepository toolRepository,
            IOrderDetailsRepository orderRepository)
        {
            _userRepository = userRepository;
            _toolRepository = toolRepository;
            _orderRepository = orderRepository;
        }

        public async Task<StatisticsDTO> GetStatisticsAsync(CancellationToken ct = default)
        {
            var users = await _userRepository.GetAllUserAsync();
            var tools = await _toolRepository.GetAllAsync();
            var orders = await _orderRepository.GetAllAsync();

            var usersList = users.ToList();
            var toolsList = tools.ToList();
            var ordersList = orders.ToList();

            return new StatisticsDTO
            {
                TotalUsers = usersList.Count,
                TotalTools = toolsList.Count,
                TotalOrders = ordersList.Count,
                ActiveOrders = ordersList.Count(o => o.Status == "Pending" || o.Status == "CheckedOut"),
                CompletedOrders = ordersList.Count(o => o.Status == "Returned")
            };
        }
    }
}
