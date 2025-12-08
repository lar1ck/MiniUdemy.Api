using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Interface
{
    public interface IEnrollementRepository
    {
        Task<List<Enrollment>> GetUserAllAsync(AppUser appUser);
        Task<List<Enrollment>> GetAllAsync();
        Task<List<Enrollment>> GetInCourseAsync(int id);
        Task<Enrollment?> GetByidAsync(int id);
        Task<Enrollment?> CreateAsync(Enrollment data);
        Task<Enrollment?> DeleteAsync(AppUser appUser, int courseId);
    }
}