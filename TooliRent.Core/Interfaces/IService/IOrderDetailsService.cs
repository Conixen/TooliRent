using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.DTO_s.OrderDetailsDTOs;

namespace TooliRent.Core.Interfaces.IService
{
    public interface IOrderDetailsService
    {
        Task<OrderDetailsDto> CreateOrderDetailsAsync(CreateOrderDTO dto);
        Task<List<OrderDetailsDto>> GetAllOrderDetailsAsync();
        Task<OrderDetailsDto> GetOrderDetailsByIdAsync(int id);
        Task UpdateOrderDetailsAsync(int id, UpdateOrderDTO dto);
        Task DeleteOrderDetailsAsync(int id);
    }
}
