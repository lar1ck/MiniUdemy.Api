using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MiniUdemy.Api.Enums;

namespace MiniUdemy.Api.Models
{
    [Table("Lesson")]
    public class Lesson
    {
        public int Id { get; set; } 
        public int ModuleId { get; set; }
        public string Title { get; set; } = string.Empty;
        public ContentTypeEnum ContentType { get; set; }
        public string? VideoUrl { get; set; }
        public string? TextContent { get; set; }
        public int OrderIndex { get; set; }
        public TimeSpan Duration { get; set; }

        public CModule Module { get; set; }

        public ICollection<LessonProgress> LessonProgresses { get; set; } = new List<LessonProgress>();
    }
}