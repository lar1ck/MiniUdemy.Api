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
        public int Id { get; set; } 
        public string UserId { get; set; } = string.Empty;
        public int LessonId { get; set; } 
        public bool IsComplete { get; set; }
        public DateTime CompletedAt { get; set; }

        [ForeignKey("UserId")]
        public AppUser Student { get; set; }
        public Lesson Lesson { get; set; }
    }
}