using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api1.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniUdemy.Api.Dtos.LessonProgress;
using MiniUdemy.Api.Interface;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Controllers
{
    [ApiController]
    [Route("api/lesson-progress")]
    public class LessonProgressController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILessonProgressRepository _lProgressRepo;
        private readonly IMapper _mapper;
        public LessonProgressController(
            UserManager<AppUser> userManager,
            ILessonProgressRepository lProgressRepo,
            IMapper mapper
        )
        {
            _userManager = userManager;
            _lProgressRepo = lProgressRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProgress()
        {
            var result = await _lProgressRepo.GetAllAsync();
            return Ok(_mapper.Map<List<LessonProgressDto>>(result));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProgressbyId([FromRoute] int id)
        {
            var result = await _lProgressRepo.GetByIdAsync(id);

            if(result == null) return NotFound("Lesson doesn't exist");

            return Ok(_mapper.Map<LessonProgressDto>(result));
        }

        [HttpPost("complete")]
        [Authorize( Roles = ("Student"))]
        public async Task<IActionResult> MarkLessonComplete(
            [FromBody] CreateLessonProgressDto data
            // [FromRoute] int id
        )
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var userName = User.Getusername();
            var appUser = await _userManager.FindByNameAsync(userName);
            var lessonProgressModel = _mapper.Map<LessonProgress>(data);
            lessonProgressModel.UserId = appUser.Id;

            var result = await _lProgressRepo.MarkAsDone(lessonProgressModel, data.LessonId);

            if (result == null) return NotFound("lesson doesn't exist");

            var newLprogress = await _lProgressRepo.GetByIdAsync(lessonProgressModel.Id);
            return Ok(_mapper.Map<LessonProgressDto>(newLprogress));
        }
    }
}