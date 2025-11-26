using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Dtos.Module
{
    public class CreateModuleDto
    {
        public string CourseId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int OrderIndex { get; set; }
    }
}