using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Dtos.Account
{
    public class LoginDto
    {
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string UserName { get; set; }

        [Required]
        public string Password {get; set;}
    }
}