using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.Models;

namespace TooliRent.Core.Interfaces.IService
{
    public interface IOrderDetailsService
    {
        Task<OrderDeatils> GetByIdAsync(int id);
        Task<List<OrderDeatils>> GetAllAsync();
        Task<OrderDeatils> AddAsync(OrderDeatils orderDeatils);
        Task UpdateAsync(OrderDeatils orderDeatils);
        Task DeleteAsync(int id);
    }

}
