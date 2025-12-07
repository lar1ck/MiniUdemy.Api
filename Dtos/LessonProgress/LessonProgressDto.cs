using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Dtos.LessonProgress
{
    public class LessonProgressDto
    {
        public int Id { get; set; } 
        public string User { get; set; } = string.Empty;  
        public string Lesson { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}