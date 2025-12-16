using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api1.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniUdemy.Api.Dtos.Review;
using MiniUdemy.Api.Interface;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Controllers
{
    [ApiController]
    [Route("api/review")]
    public class ReviewController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepo;
        public ReviewController(
           UserManager<AppUser> userManager,
           IMapper mapper,
           IReviewRepository reviewRepo
        )
        {
            _userManager = userManager;
            _mapper = mapper;
            _reviewRepo = reviewRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _reviewRepo.GetAllAsync();
            return Ok(_mapper.Map<List<ReviewDto>>(reviews));
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetReview([FromRoute] int id)
        {
            var review = await _reviewRepo.GetByIdAsync(id);

            if (review == null) return NotFound("Review doesn't exist");
            return Ok(_mapper.Map<ReviewDto>(review));
        }

        [HttpPost("create")]
        [Authorize(Roles = ("Student"))]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userName = User.Getusername();
            var appuser = await _userManager.FindByNameAsync(userName);

            var reviewModel = _mapper.Map<Review>(data);
            reviewModel.UserId = appuser.Id;

            var canReview = _reviewRepo.HasIncompleteLessons(appuser, data.CourseId);
            if (canReview == true) return BadRequest("You have to complete the course to give a review");

            var result = await _reviewRepo.CreateAsync(reviewModel);
            if (result == null) return NotFound("Course doesn't exist"); 

            
            var newReview = await _reviewRepo.GetByIdAsync(result.Id);

            return CreatedAtAction(nameof(GetReview), new { id = result.Id }, _mapper.Map<ReviewDto>(newReview));
        }

        [HttpDelete("delete/{id:int}")]
        [Authorize(Roles = ("Student"))]
        public async Task<IActionResult> DeleteReview([FromRoute] int id)
        {
            var userName = User.Getusername();
            var appuser = await _userManager.FindByNameAsync(userName);

            var result = await _reviewRepo.DeleteAsync(appuser, id);

            if (result == null) return NotFound("Review doesn't exist");

            return NoContent();
        }

        [HttpGet("/course/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetCourseReviews([FromRoute] int id)
        {
            var reviews = await _reviewRepo.GetByCourseIdAsync(id);
            return Ok(_mapper.Map<List<ReviewDto>>(reviews));
        }
    }
}