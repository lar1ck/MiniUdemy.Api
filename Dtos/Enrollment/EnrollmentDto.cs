using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Dtos.Enrollment
{
    public class EnrollmentDto
    {
        public int Id { get; set; }
        public string User { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
    }
}