using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniUdemy.Api.Dtos.LessonProgress;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Interface
{
    public interface ILessonProgressRepository
    {
        Task<List<LessonProgress>> GetAllAsync();
        Task<List<LessonProgress>> GetUserAsync(AppUser appUser);
        Task<LessonProgress?> GetByIdAsync(int id);
        Task<LessonProgress?> MarkAsDone(int lessonId, AppUser appUser);
    }
}