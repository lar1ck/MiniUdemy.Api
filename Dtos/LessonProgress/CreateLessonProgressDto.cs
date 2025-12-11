using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Dtos.LessonProgress
{
    public class CreateLessonProgressDto
    {
        public int LessonId { get; set; }  
        // public bool IsComplete { get; set; } = true;
        // public DateTime CompletedAt { get; set; } = DateTime.Now;
    }
}