using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Models
{
    [Table("LessonProgress")]
    public class LessonProgress
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string LessonId { get; set; }
        public bool IsComplete { get; set; }
        public DateTime CompletedAt { get; set; }

        public AppUser Student { get; set; }
        public Lesson Lesson { get; set; }
    }
}