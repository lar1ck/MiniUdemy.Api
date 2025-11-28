using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Models
{
    [Table("Review")]
    public class Review
    {
        public int Id { get; set; } 
        public int CourseId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Course Course { get; set; }
        
        [ForeignKey("UserId")]
        public AppUser User { get; set; }
    }
}