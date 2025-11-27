using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniUdemy.Api.Data;
using MiniUdemy.Api.Interface;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Course> CreateAsync(Course data)
        {
            await _context.Course.AddAsync(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _context.Course.ToListAsync();

        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Course.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}