using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniUdemy.Api.Data;
using MiniUdemy.Api.Dtos.Category;
using MiniUdemy.Api.Interface;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category data)
        {
            await _context.Category.AddAsync(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<Category?> DeleteAsync(int Id)
        {
            var category = await _context.Category.FirstOrDefaultAsync(c => c.Id == Id);

            if(category == null) return null;

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<Category?> GetIdAsync(int id)
        {
            return await _context.Category.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> UpdateAsync(Category data, int Id)
        {
            var category = await _context.Category.FirstOrDefaultAsync(c => c.Id == Id);

            if(category == null)
            {
                return null;
            }

            category.Name = data.Name;
            category.Slug = data.Slug;

            await _context.SaveChangesAsync();
            return category;
        }
    }
}