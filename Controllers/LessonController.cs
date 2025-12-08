using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api1.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MiniUdemy.Api.Dtos.Lesson;
using MiniUdemy.Api.Interface;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Controllers
{
    [ApiController]
    [Route("api/lesson")]
    public class LessonController : ControllerBase
    {
        private readonly ILessonRepository _lessonRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public LessonController(
            ILessonRepository lessonRepo, 
            IMapper mapper, 
            UserManager<AppUser> userManager)
        {
            _lessonRepo = lessonRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("admin")]
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> GetAllLessons()
        {
            var lessons = await _lessonRepo.GetAllAsync();
            return Ok(_mapper.Map<List<LessonDto>>(lessons));
        }

        [HttpGet]
        [Authorize(Roles = ("Student"))]
        public async Task<IActionResult> GetUserLessons()
        {
            var userName = User.Getusername();
            var appUser = await _userManager.FindByNameAsync(userName);

            var lessons = await _lessonRepo.GetUserLessonslAsync(appUser);
            return Ok(_mapper.Map<List<LessonDto>>(lessons));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> GetLesson([FromRoute] int id)
        {
            var lesson = await _lessonRepo.GetByIdAsync(id);

            if (lesson == null) return NotFound("Lesson doesn't exsit");

            return Ok(_mapper.Map<LessonDto>(lesson));
        }

        [HttpPost("create")]
        [Authorize(Roles = ("Tutor"))]
        public async Task<IActionResult> CreateLesson([FromBody] CreateLessonDto data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var lessonModel = _mapper.Map<Lesson>(data);
            var result = await _lessonRepo.CreateAsync(lessonModel);

            var lesson = await _lessonRepo.GetByIdAsync(result.Id);

            return CreatedAtAction(nameof(GetLesson), new { id = lessonModel.Id }, _mapper.Map<LessonDto>(lesson));
        }

        [HttpPut("update/{id:int}")]
        [Authorize(Roles = ("Tutor"))] // make sur it is the woner
        public async Task<IActionResult> UpdateLesson([FromBody] UpdateLessonDto data, [FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var lessonModel = _mapper.Map<Lesson>(data);
            var result = await _lessonRepo.UpdateAsync(lessonModel, id);

            if (result == null) return NotFound("Lesson doesn't exist");

            var lesson = await _lessonRepo.GetByIdAsync(result.Id);

            return Ok(_mapper.Map<LessonDto>(lesson));
        }

        [HttpDelete("delete/{id:int}")]
        [Authorize(Roles = ("Tutor"))] // make sur it is the woner
        public async Task<IActionResult> DeleteLesson([FromRoute] int id)
        {
            var result = await _lessonRepo.DeleteAsync(id);

            if (result == null) return NotFound("Lesson doesn't exist");
            return NoContent();
        }
    }
}