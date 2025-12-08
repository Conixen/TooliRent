using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.DTO_s.OrderDetailsDTOs;
using TooliRent.Models;
namespace TooliRent.Core.Interfaces.IRepository
{
    public interface IOrderDetailsRepository
    {
        Task<IEnumerable<OrderDetails>> GetAllAsync();
        Task<OrderDetails?> GetByIdAsync(int id);
        Task<OrderDetails> CreateAsync(OrderDetails order);
        Task UpdateAsync(OrderDetails order);
        Task DeleteAsync(int id);

        //Task<List<OrderDeatils>> GetByUserIdAsync(int userId);
        //Task<List<OrderDeatils>> GetActiveOrdersAsync();
        //Task<List<OrderDeatils>> GetOverdueOrdersAsync();
    }
}
