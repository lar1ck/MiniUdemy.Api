using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Dtos.Course
{
    public class DisplayCourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Instructor { get; set; } = string.Empty;
        public bool isActive {get; set;} = true;
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = null;
    }
}