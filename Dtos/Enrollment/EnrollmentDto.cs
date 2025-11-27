using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Dtos.Enrollment
{
    public class EnrollmentDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string CourseId { get; set; } = string.Empty;
    }
}