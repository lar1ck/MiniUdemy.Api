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
    public class EnrollementRepository : IEnrollementRepository
    {
        private readonly ApplicationDbContext _context;
        public EnrollementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Enrollment?> CreateAsync(Enrollment data)
        {
            var isEnrolled = await _context.Enrollment.AnyAsync(r => r.UserId == data.UserId && r.CourseId == data.CourseId);
            
            if(isEnrolled) return null;
            
            await _context.Enrollment.AddAsync(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<Enrollment?> DeleteAsync(AppUser appUser, int courseId)
        {
            var enrollement = await _context.Enrollment.
                                        FirstOrDefaultAsync(
                                            e => e.CourseId == courseId && 
                                            e.UserId == appUser.Id
                                        );

            if(enrollement == null) return null;

            _context.Enrollment.Remove(enrollement);
            await _context.SaveChangesAsync();
            return enrollement;
        }

        public async Task<List<Enrollment>> GetAllAsync(AppUser appUser)
        {
            return await _context.Enrollment
                            .Include(e => e.Course)
                            .Include(e => e.Student).Where(e => e.UserId == appUser.Id).ToListAsync();
        }

        public async Task<Enrollment?> GetByidAsync(int id)
        {
            var enrollement = await _context.Enrollment
                            .Include(e => e.Course)
                            .Include(e => e.Student).FirstOrDefaultAsync(e => e.Id == id);

            if (enrollement == null) return null;

            return enrollement;
        }
    }
}