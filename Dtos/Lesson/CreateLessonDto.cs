using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniUdemy.Api.Enums;

namespace MiniUdemy.Api.Dtos.Lesson
{
    public class CreateLessonDto
    {
        public int ModuleId { get; set; }
        public string Title { get; set; } = string.Empty;
        public ContentTypeEnum ContentType { get; set; }
        public string? VideoUrl { get; set; }
        public string? TextContent { get; set; }
        public int OrderIndex { get; set; }
        public TimeSpan Duration { get; set; }
    }
}