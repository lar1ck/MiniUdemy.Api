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
    public class LessonRepository : ILessonRepository
    {
        private readonly ApplicationDbContext _context;
        public LessonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Lesson?> CreateAsync(Lesson data, AppUser appUser)
        {
            var module = await _context.Module.Include(m => m.Course).FirstOrDefaultAsync(m => m.Id == data.ModuleId);

            if(module == null || module.Course.UserId != appUser.Id) return null;

            await _context.Lesson.AddAsync(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<Lesson?> DeleteAsync(int id, AppUser appUser)
        {
            var lesson = await _context.Lesson
                                    .Include(l => l.Module)
                                    .ThenInclude(m => m.Course)
                                    .FirstOrDefaultAsync(l => l.Id == id);

            if (lesson == null || lesson.Module.Course.UserId != appUser.Id) return null;

            _context.Lesson.Remove(lesson);
            await _context.SaveChangesAsync();
            return lesson;
        }

        public async Task<List<Lesson>> GetAllAsync()
        {
            return await _context.Lesson.Include(l => l.Module).ToListAsync();
        }

        public async Task<Lesson?> GetByIdAsync(int id)
        {
            var lesson = await _context.Lesson.Include(l => l.Module).FirstOrDefaultAsync(l => l.Id == id);

            if (lesson == null) return null;

            return lesson;
        }

        public async Task<List<Lesson>> GetUserLessonslAsync(AppUser appUser)
        {
            return await _context.Lesson
                            .Include(l => l.Module)
                            .ThenInclude(m => m.Course)
                            .Where(c => c.Module.Course.EnrolledStudents.Any(e => e.UserId == appUser.Id))
                            .ToListAsync();
        }

        public async Task<Lesson?> UpdateAsync(Lesson data, int id, AppUser appUser)
        {
            var lesson = await _context.Lesson
                                    .Include(l => l.Module)
                                    .ThenInclude(m => m.Course)
                                    .FirstOrDefaultAsync(l => l.Id == id);

            if (lesson == null || lesson.Module.Course.UserId != appUser.Id) return null;

            lesson.ModuleId = data.ModuleId;
            lesson.Title = data.Title;
            lesson.ContentType = data.ContentType;
            lesson.VideoUrl = data.VideoUrl;
            lesson.TextContent = data.TextContent;
            lesson.OrderIndex = data.OrderIndex;
            lesson.Duration = data.Duration;

            await _context.SaveChangesAsync();
            return lesson;
        }
    }
}