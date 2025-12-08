using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniUdemy.Api.Data;
using MiniUdemy.Api.Models;
using MiniUdemy.Api.Interface;
using MiniUdemy.Api.Dtos.Module;

namespace MiniUdemy.Api.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly ApplicationDbContext _context;
        public ModuleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CModule> CreateAsync(CModule data)
        {
            await _context.Module.AddAsync(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<CModule?> DeleteAsync(int id)
        {
            var module = await _context.Module.FirstOrDefaultAsync(m => m.Id == id);

            if(module == null ) return null;

            _context.Module.Remove(module);
            await _context.SaveChangesAsync();
            return module;
        }

        public async Task<List<CModule>> GetAllAsync()
        {
            return await _context.Module.Include(m => m.Course).ToListAsync();
        }

        public async Task<CModule?> GetByIdAsync(int id)
        {
            return await _context.Module.Include(m => m.Course).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<CModule>> GetUserModulesAsync(AppUser appUser)
        {
            return await _context.Module.Include(m => m.Course).Where(m => m.Course.EnrolledStudents.Any(e => e.UserId == appUser.Id)).ToListAsync();
        }

        public async Task<CModule?> UpadteAsync(CModule data, int id, AppUser appUser)
        {
            var module = await _context.Module
                                    .Include(m => m.Course)
                                    .Where(m => m.Course.UserId == appUser.Id)
                                    .FirstOrDefaultAsync(m => m.Id == id);

            if(module == null) return null;

            module.CourseId = data.CourseId;
            module.Title = data.Title;
            module.OrderIndex = data.OrderIndex;

            await _context.SaveChangesAsync();
            return module;
        }
    }
}