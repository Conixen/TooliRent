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
        Task<IEnumerable<OrderDeatils>> GetAllAsync();
        Task<OrderDeatils?> GetByIdAsync(int id);
        Task<OrderDeatils> CreateAsync(OrderDeatils order);
        Task UpdateAsync(OrderDeatils order);
        Task DeleteAsync(int id);

        //Task<List<OrderDeatils>> GetByUserIdAsync(int userId);
        //Task<List<OrderDeatils>> GetActiveOrdersAsync();
        //Task<List<OrderDeatils>> GetOverdueOrdersAsync();
    }
}
