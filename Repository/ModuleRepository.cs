using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniUdemy.Api.Data;
using MiniUdemy.Api.Models;
using MiniUdemy.Api.Interface;

namespace MiniUdemy.Api.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly ApplicationDbContext _context;
        public ModuleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<CModule>> GetAllAsync()
        {
            return await _context.Module.Include(m => m.Course).ToListAsync();
        }
    }
}