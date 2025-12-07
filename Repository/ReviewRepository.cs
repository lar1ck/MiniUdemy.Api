using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniUdemy.Api.Data;
using MiniUdemy.Api.Dtos.Review;
using MiniUdemy.Api.Interface;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Review?> CreateAsync(Review data)
        {
            var course = await _context.Course.FirstOrDefaultAsync(c => c.Id == data.CourseId);

            if(course == null) return null;

            await _context.Review.AddAsync(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<Review?> DeleteAsync(int id)
        {
            var review = await _context.Review.FirstOrDefaultAsync(r => r.Id == id);

            if (review == null) return null;

            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Review.Include(r => r.Course).Include(r => r.User).ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            var review = await _context.Review
                            .Include(r => r.Course)
                            .Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id);

            if (review == null) return null;

            return review;
        }
    }
}