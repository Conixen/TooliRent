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

        public async Task<OrderDeatils?> GetByIdAsync(int id)
        {
            return await _context.OrderDeatils
                .Include(o => o.User)
                .Include(o => o.Tool)
                    .ThenInclude(t => t.Category)
                .Include(o => o.Reservation)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<OrderDeatils>> GetAllAsync()
        {
            return await _context.OrderDeatils
                .Include(o => o.User)
                .Include(o => o.Tool)
                    .ThenInclude(t => t.Category)
                .Include(o => o.Reservation)
                .ToListAsync();
        }

        public async Task<List<OrderDeatils>> GetByUserIdAsync(int userId)
        {
            return await _context.OrderDeatils
                .Include(o => o.User)
                .Include(o => o.Tool)
                    .ThenInclude(t => t.Category)
                .Include(o => o.Reservation)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<OrderDeatils>> GetActiveOrdersAsync()
        {
            return await _context.OrderDeatils
                .Include(o => o.User)
                .Include(o => o.Tool)
                .Include(o => o.Reservation)
                .Where(o => o.Status == "CheckedOut" || o.Status == "Pending")
                .ToListAsync();
        }

        public async Task<List<OrderDeatils>> GetOverdueOrdersAsync()
        {
            return await _context.OrderDeatils
                .Include(o => o.User)
                .Include(o => o.Tool)
                .Include(o => o.Reservation)
                .Where(o => o.Date2Return < DateTime.UtcNow && o.Status != "Returned")
                .ToListAsync();
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
