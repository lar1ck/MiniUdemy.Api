using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MiniUdemy.Api.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Course> CoursesTaught { get; set; } = new List<Course>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<LessonProgress> LessonProgresses { get; set; } = new List<LessonProgress>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}