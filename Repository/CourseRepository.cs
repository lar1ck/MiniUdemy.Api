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

        public async Task<Course?> CreateAsync(Course data, AppUser appUser)
        {
            var isCourseActive = _context.Course
                                            .Where(
                                                c => c.UserId == appUser.Id &&
                                                c.Title == data.Title
                                            ).Any();

            if (!isCourseActive)
            {
                await _context.Course.AddAsync(data);
                await _context.SaveChangesAsync();
                return data;
            }

            return null;
        }

        public async Task<Course?> DeleteAsync(int id, AppUser appUser)
        {
            var course = await _context.Course.Where(c => c.UserId == appUser.Id).FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return null;

            course.isActive = false;
            course.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _context.Course.Include(c => c.Instructor).Include(c => c.Category).ToListAsync();

        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Course.Include(c => c.Instructor).Include(c => c.Category).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course?> UpdateAsync(Course data, int id, AppUser appUser)
        {
            var course = await _context.Course
                                    .Include(c => c.Instructor)
                                    .Include(c => c.Category)
                                    .Where(c => c.UserId == appUser.Id)
                                    .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return null;

            course.Title = data.Title;
            course.Description = data.Description;
            course.Thumbnail = data.Thumbnail;
            course.CategoryId = data.CategoryId;
            course.Price = data.Price;
            course.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return course;
        }
    }
}