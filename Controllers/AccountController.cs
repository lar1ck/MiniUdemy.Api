using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniUdemy.Api.Dtos.Account;
using MiniUdemy.Api.Interface;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
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

                var createUser = await _userManager.CreateAsync(user, registerDto.Password);

                if (createUser.Succeeded)
                {
                    var addRole = await _userManager.AddToRoleAsync(user, "Admin");
                    if (addRole.Succeeded)
                    {
                        return Ok(new NewUserDto
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            Token = _tokenService.CreateToken(user)
                        });
                    }
                    else
                    {
                        return StatusCode(500, addRole.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createUser.Errors);
                }
            }
            catch(Exception e)
            {
                return StatusCode(500, new {message = "Something went wrong", err = e.Message});
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginData)
        {
            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == loginData.UserName.ToLower());

            if (user == null) return Unauthorized("Username or Password are incorrect");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginData.Password, false);

            if (!result.Succeeded) return Unauthorized("Username or Password are incorrect");

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }
    }
}