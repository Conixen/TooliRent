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

        public async Task<IEnumerable<OrderDetails>> GetAllAsync()
        {
            return await _context.OrderDeatils
                .Include(o => o.User)
                .Include(o => o.Tool)
                .ToListAsync();
        }

        public async Task<OrderDetails?> GetByIdAsync(int id)
        {
            return await _context.OrderDeatils
                .Include(o => o.User)
                .Include(o => o.Tool)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<OrderDetails> CreateAsync(OrderDetails order)
        {
            await _context.OrderDeatils.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task UpdateAsync(OrderDetails order)
        {
            _context.OrderDeatils.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.OrderDeatils.FindAsync(id);
            if (order != null)
            {
                _context.OrderDeatils.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
