using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Interface
{
    public interface IEnrollementRepository
    {
        Task<List<Enrollment>> GetAllAsync(AppUser appUser);
        Task<Enrollment?> GetByidAsync(int id);
        Task<Enrollment?> CreateAsync(Enrollment data);
        Task<Enrollment?> DeleteAsync(AppUser appUser, int courseId);
    }
}