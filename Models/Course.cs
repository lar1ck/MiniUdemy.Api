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
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public string CategoryId { get; set; }
        public string InstructorId { get; set; }
        public bool isActive {get; set;} = true;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Category Category { get; set; }
        public AppUser Instructor { get; set; }

        public ICollection<Module> Modules { get; set; } = new List<Module>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Enrollment> EnrolledStudents { get; set; } = new List<Enrollment>();
    }
}