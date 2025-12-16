using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Dtos.Course
{
    public class CourseQueryObject
    {
        public string? Title { get; set; } = string.Empty;
        public string? Category { get; set; } = string.Empty;
        public string? Instructor { get; set; } = string.Empty;
        public bool? isActive {get; set;}
        public decimal? CheaperThan { get; set; }
        [Range(0, double.MaxValue)]
        public decimal? HigherThan { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
    }
}