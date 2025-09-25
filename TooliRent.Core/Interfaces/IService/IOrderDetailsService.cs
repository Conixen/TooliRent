using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.DTO_s.OrderDetailsDTOs;
using TooliRent.Models;

namespace TooliRent.Core.Interfaces.IService
{
    public interface IOrderDetailsService
    {
        // Basic CRUD
        Task<OrderSummaryDTO?> GetByIdAsync(int id);
        Task<List<OrderSummaryDTO>> GetAllAsync();
        Task<List<OrderSummaryDTO>> GetByUserIdAsync(int userId);
        Task DeleteAsync(int id);   // admin only

        // Create & Update
        Task<OrderSummaryDTO> CreateAsync(CreateOrderDTO dto, int userId);
        Task UpdateAsync(int id, UpdateOrderDTO dto);

        // Order lifecycle
        Task CheckOutAsync(int id);
        Task ReturnAsync(int id);
        Task CancelAsync(int id, OrderCancelDTO dto);

        // Business logic
        Task<List<OrderSummaryDTO>> GetActiveOrdersAsync();
        Task<List<OrderSummaryDTO>> GetOverdueOrdersAsync();
        Task<decimal> CalculateLateFeeAsync(int orderId);

    }
}

}
