using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Interface
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task<Course> CreateAsync(Course data);

        Task<Course?> DeleteAsync(int id);
    }
}