using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Dtos.Review
{
    public class ReviewDto
    {
        public string Id { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}