using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniUdemy.Api.Dtos.Category;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Interface
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetIdAsync(int id);
        Task<Category?> UpdateAsync(Category data, int Id);
        Task<Category> CreateAsync(Category data);
        Task<Category?> DeleteAsync(int Id);
    }
}