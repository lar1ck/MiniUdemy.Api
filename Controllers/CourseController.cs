using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api1.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniUdemy.Api.Dtos.Course;
using MiniUdemy.Api.Interface;
using MiniUdemy.Api.Models;
using MiniUdemy.Api.Repository;

namespace MiniUdemy.Api.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public CourseController
        (
            ICourseRepository courseRepo, 
            IMapper mapper,
            UserManager<AppUser> userManager 
        )
        {
            _courseRepo = courseRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseRepo.GetAllAsync();
            var cleanCourses = _mapper.Map<List<DisplayCourseDto>>(courses);
            return Ok(cleanCourses);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetCourse([FromRoute] int id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
            if(course == null) return NotFound("Course doesn't exist");
            return Ok(_mapper.Map<DisplayCourseDto>(course));
        }

        [HttpPost("create")]
        [Authorize(Roles = ("Admin, Tutor"))]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto courseData)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var username = User.Getusername();
            var appUser = await _userManager.FindByNameAsync(username);

            var courseModel = _mapper.Map<Course>(courseData);
            courseModel.UserId = appUser.Id;
            await _courseRepo.CreateAsync(courseModel);

            return CreatedAtAction(nameof(GetCourse), new {id = courseModel.Id}, _mapper.Map<CourseDto>(courseModel));
        }
    }
}