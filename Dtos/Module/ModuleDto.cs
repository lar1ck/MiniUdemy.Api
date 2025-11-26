using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Dtos.Module
{
    public class ModuleDto
    {
        public string Id { get; set; } = string.Empty;
        public string CourseId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int OrderIndex { get; set; }
    }
}