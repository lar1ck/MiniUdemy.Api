using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api1.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniUdemy.Api.Dtos.Enrollment;
using MiniUdemy.Api.Interface;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Controllers
{
    [ApiController]
    [Route("api/enrollement")]
    public class EnrollementController : ControllerBase
    {
        readonly IEnrollementRepository _enrollementRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICourseRepository _courseRepo;
        public EnrollementController(
            IEnrollementRepository enrollementRepo,
            IMapper mapper,
            UserManager<AppUser> userManager,
            ICourseRepository courseRepo
        )
        {
            _enrollementRepo = enrollementRepo;
            _mapper = mapper;
            _userManager = userManager;
            _courseRepo = courseRepo;
        }

        //Later add seeing students in a course

        [HttpGet]
        [Authorize(Roles = ("Admin, Tutor"))]
        public async Task<IActionResult> GetAllEnrollements()
        {
            var userName = User.Getusername();
            var appUser = await _userManager.FindByNameAsync(userName);

            var enrollements = await _enrollementRepo.GetAllAsync(appUser);
            return Ok(_mapper.Map<List<EnrollmentDto>>(enrollements));
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = ("Admin, Tutor"))]
        public async Task<IActionResult> GetEnrollementById([FromRoute] int id)
        {
            var enrollement = await _enrollementRepo.GetByidAsync(id);

            if (enrollement == null) return NotFound("Enrollement doesn't exist");
            return Ok(_mapper.Map<EnrollmentDto>(enrollement));
        }

        [HttpPost("enroll")]
        [Authorize(Roles = ("Student"))]
        public async Task<IActionResult> EnrollToCourse([FromBody] EnrollDto data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userName = User.Getusername();
            var appUser = await _userManager.FindByNameAsync(userName);

            var enrollModel = _mapper.Map<Enrollment>(data);
            enrollModel.UserId = appUser.Id;

            if (await _courseRepo.GetByIdAsync(data.CourseId) == null)
            {
                return NotFound("Course doesn't exist");
            }

            var result = await _enrollementRepo.CreateAsync(enrollModel);

            if (result == null) return BadRequest("You are already enrolled");

            var newEnrollement = await _enrollementRepo.GetByidAsync(result.Id);
            return CreatedAtAction(nameof(GetEnrollementById), new { id = result.Id }, _mapper.Map<EnrollmentDto>(newEnrollement));
        }

        [HttpDelete("withdraw/{courseId:int}")]
        [Authorize(Roles = ("Student"))]
        public async Task<IActionResult> WithdrawFromCourse(
            [FromRoute] int courseId
        )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userName = User.Getusername();
            var appUser = await _userManager.FindByNameAsync(userName);

            var result = await _enrollementRepo.DeleteAsync(appUser, courseId);
            if (result == null) return BadRequest("You are not enrolled in this course");

            return NoContent();
        }
    }
}