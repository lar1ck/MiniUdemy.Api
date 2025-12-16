using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniUdemy.Api.Data;
using MiniUdemy.Api.Dtos.Course;
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

        public async Task<List<Course>> GetAllAsync(CourseQueryObject query)
        {
            var courses = _context.Course
                                        .Include(c => c.Instructor)
                                        .Include(c => c.Category)
                                        .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                courses = courses.Where(c => c.Category.Name == query.Category);
            }
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                courses = courses.Where(c => c.Title == query.Title);
            }
            if (!string.IsNullOrWhiteSpace(query.Instructor))
            {
                courses = courses.Where(c => c.Instructor.UserName == query.Instructor);
            }
            if (query.CheaperThan.HasValue)
            {
                courses = courses.Where(c => c.Price <= query.CheaperThan);
            }
            if (query.HigherThan.HasValue)
            {
                courses = courses.Where(c => c.Price >= query.HigherThan);
            }
            if (query.isActive.HasValue)
            {
                courses = courses.Where(c => c.isActive == query.isActive);
            }
            if (query.CreatedBefore.HasValue)
            {
                courses = courses.Where(c => c.CreatedAt < query.CreatedBefore);
            }
            if (query.CreatedAfter.HasValue)
            {
                courses = courses.Where(c => c.CreatedAt > query.CreatedAfter);
            }

            return await courses.ToListAsync();

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