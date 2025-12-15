using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniUdemy.Api.Dtos.Review;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Interface
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(int id);
        Task<Review?> CreateAsync(Review data);
        Task<Review?> DeleteAsync(int id);
        public bool HasIncompleteLessons(AppUser appUser, int courseId);
    }
}