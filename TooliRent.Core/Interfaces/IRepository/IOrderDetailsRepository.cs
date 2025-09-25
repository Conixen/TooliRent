using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.Models;
namespace TooliRent.Core.Interfaces.IRepository
{
    public interface IOrderDetailsRepository
    {
        Task<OrderDeatils?> GetByIdAsync(int id);
        Task<List<OrderDeatils>> GetAllAsync();
        Task<List<OrderDeatils>> GetByUserIdAsync(int userId);
        Task<List<OrderDeatils>> GetActiveOrdersAsync();
        Task<List<OrderDeatils>> GetOverdueOrdersAsync();
        Task<OrderDeatils> AddAsync(OrderDeatils orderDeatils);
        Task UpdateAsync(OrderDeatils orderDeatils);
        Task DeleteAsync(int id);
    }
}
