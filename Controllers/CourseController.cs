using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniUdemy.Api.Dtos.Course;
using MiniUdemy.Api.Interface;
using MiniUdemy.Api.Models;
using MiniUdemy.Api.Repository;

namespace MiniUdemy.Api.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseController: ControllerBase
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IMapper _mapper;
        public CourseController(ICourseRepository courseRepo, IMapper mapper)
        {
            _courseRepo = courseRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllCourses()
        {
            var comments = await _courseRepo.GetAllAsync();
            var cleanComments = _mapper.Map<List<CourseDto>>(comments);
            return Ok(cleanComments);
        }
    }
}