using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Dtos.LessonProgress
{
    public class LessonProgressDto
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; }  = string.Empty;
        public string LessonId { get; set; }  = string.Empty;
        public bool IsComplete { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}