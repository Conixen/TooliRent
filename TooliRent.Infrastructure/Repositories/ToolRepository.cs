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
    public class ToolRepository : IToolRepository
    {
        private readonly TooliRentContext _context;
        public ToolRepository(TooliRentContext context)
        {
            _context = context;
        }
        public async Task<Tool> GetByIdAsync(int id)
        {
            return await _context.Tools.FindAsync(id);
        }
        public async Task<List<Tool>> GetAllAsync()
        {
            return await _context.Tools.ToListAsync();
        }
        public async Task<Tool> AddAsync(Tool tool)
        {
            _context.Tools.Add(tool);
            await _context.SaveChangesAsync();
            return tool;
        }
        public async Task UpdateAsync(Tool tool)
        {
            _context.Tools.Update(tool);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var tool = await _context.Tools.FindAsync(id);
            if (tool != null)
            {
                _context.Tools.Remove(tool);
                await _context.SaveChangesAsync();
            }
        }
    }
}
