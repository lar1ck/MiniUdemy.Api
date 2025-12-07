using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Interface
{
    public interface ILessonRepository
    {
        Task<List<Lesson>> GetAllAsync();
        Task<Lesson?> GetByIdAsync(int id);
        Task<Lesson> CreateAsync(Lesson data);
        Task<Lesson?> UpdateAsync(Lesson data, int id);
        Task<Lesson?> DeleteAsync(int id);
    }
}