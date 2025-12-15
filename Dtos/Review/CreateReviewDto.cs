using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Dtos.Review
{
    public class CreateReviewDto
    { 
        public int CourseId { get; set; }
        [Range(1,5)]
        public int Rating { get; set; }
        [MaxLength(150)]
        public string? Comment { get; set; } = string.Empty;
    }
}