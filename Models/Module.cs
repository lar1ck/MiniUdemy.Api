using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Models
{
    [Table("Module")]
    public class Module
    {
        public int Id { get; set; } 
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int OrderIndex { get; set; }

        public Course Course { get; set; }

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}