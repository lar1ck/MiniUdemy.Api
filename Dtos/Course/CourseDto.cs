using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Dtos.Course
{
    public class CourseDto
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        [StringLength(300)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(40)]
        public string Thumbnail { get; set; } = string.Empty;
        
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string InstructorId { get; set; } = string.Empty;
        public bool isActive {get; set;} = true;

        [Required]
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = null;

    }
}