using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Models
{
    [Table("Course")]
    public class Course
    {
        public int Id { get; set; } 
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public bool isActive {get; set;} = true;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = null;

        public Category Category { get; set; }
        
        [ForeignKey("UserId")]
        public AppUser Instructor { get; set; }

        public ICollection<CModule> Modules { get; set; } = new List<CModule>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Enrollment> EnrolledStudents { get; set; } = new List<Enrollment>();
    }
}