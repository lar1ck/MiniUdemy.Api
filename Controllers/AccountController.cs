using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniUdemy.Api.Dtos.Account;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAccount([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.Phone
                };

                var creaetUser = await _userManager.CreateAsync(user, registerDto.Password);

                if (creaetUser.Succeeded)
                {
                    var addRole = await _userManager.AddToRoleAsync(user, "Admin");
                    if (addRole.Succeeded)
                    {
                        return Ok("User Created");
                    }
                    else
                    {
                        return StatusCode(500, addRole.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, creaetUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500,  "Something went wrong. Please try again.");
            }

        }
    }
}