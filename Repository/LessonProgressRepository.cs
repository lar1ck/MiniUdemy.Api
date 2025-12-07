using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniUdemy.Api.Data;
using MiniUdemy.Api.Dtos.LessonProgress;
using MiniUdemy.Api.Interface;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Repository
{
    public class LessonProgressRepository : ILessonProgressRepository
    {
        private readonly ApplicationDbContext _context;
        public LessonProgressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LessonProgress>> GetAllAsync()
        {
            return await _context.LessonProgress
                                .Include(l => l.Lesson)
                                .Include(l => l.Student).ToListAsync();
        }

        public async Task<LessonProgress?> GetByIdAsync(int id)
        {
            var result = await _context.LessonProgress
                                .Include(l => l.Lesson)
                                .Include(l => l.Student).FirstOrDefaultAsync(l => l.Id == id);
            
            if(result == null) return null;
            return result;
        }

        public async Task<LessonProgress?> MarkAsDone(LessonProgress data, int id)
        {
            var lesson = await _context.Lesson.FirstOrDefaultAsync(l => l.Id == id);

            if(lesson == null) return null;

            await _context.LessonProgress.AddAsync(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}