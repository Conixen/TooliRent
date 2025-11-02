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
        Task<IEnumerable<OrderDetailsDTO>> GetAllAsync(CancellationToken ct = default);
        Task<OrderDetailsDTO?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<OrderDetailsDTO> CreateAsync(CreateOrderDTO create, CancellationToken ct = default);
        Task<OrderDetailsDTO?> UpdateAsync(int id, UpdateOrderDTO update, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);

        // Order lifecycle
        //Task CheckOutAsync(int id);
        //Task ReturnAsync(int id);
        //Task CancelAsync(int id, OrderCancelDTO dto);

        //// Business logic
        //Task<List<OrderSummaryDTO>> GetActiveOrdersAsync();
        //Task<List<OrderSummaryDTO>> GetOverdueOrdersAsync();
        //Task<decimal> CalculateLateFeeAsync(int orderId);

    }
}


