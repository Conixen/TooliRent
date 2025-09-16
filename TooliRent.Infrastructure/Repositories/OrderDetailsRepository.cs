using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.Core.Interfaces.IRepository;
using TooliRent.Models;
using TooliRent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TooliRent.Infrastructure.Repositories
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly TooliRentContext _context;
        public OrderDetailsRepository(TooliRentContext context)
        {
            _context = context;
        }

        public async Task<OrderDeatils> GetByIdAsync(int id)
        {
            return await _context.OrderDeatils.FindAsync(id);
        }
        public async Task<List<OrderDeatils>> GetAllAsync()
        {
            return await _context.OrderDeatils.ToListAsync();
        }
        public async Task<OrderDeatils> AddAsync(OrderDeatils orderDeatils)
        {
            _context.OrderDeatils.Add(orderDeatils);
            await _context.SaveChangesAsync();
            return orderDeatils;
        }
        public async Task UpdateAsync(OrderDeatils orderDeatils)
        {
            _context.OrderDeatils.Update(orderDeatils);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var orderDeatils = await _context.OrderDeatils.FindAsync(id);
            if (orderDeatils != null)
            {
                _context.OrderDeatils.Remove(orderDeatils);
                await _context.SaveChangesAsync();
            }
        }
    }
}
