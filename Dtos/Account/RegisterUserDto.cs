using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Dtos.Account
{
    public class RegisterUserDto
    {
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        [Display(Name = "John Doe")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "email@example.com")]
        public string Email { get; set; }

        [Required]
        public string Password {get; set;}
        public string Phone { get; set; }
    }
}