using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Models
{
    [Table("Enrollment")]
    public class Enrollment
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CourseId { get; set; }

        public AppUser Student { get; set; }
        public Course Course { get; set; }
    }
}