using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.Infrastructure.Data;
using TooliRent.Models;
using TooliRent.Core.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace TooliRent.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TooliRentContext _context;
        
        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categorys.FindAsync(id);
        }
        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categorys.ToListAsync();
        }
        public async Task<Category> AddAsync(Category category)
        {
            _context.Categorys.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task UpdateAsync(Category category)
        {
            _context.Categorys.Update(category);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categorys.FindAsync(id);
            if (category != null)
            {
                _context.Categorys.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
