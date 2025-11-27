using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MiniUdemy.Api.Dtos.Category;
using MiniUdemy.Api.Dtos.Course;
using MiniUdemy.Api.Interface;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return Ok(_mapper.Map<List<CategoryDto>>(categories));
        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryRepo.GetIdAsync(id);
            if (category == null)
            {
                return NotFound("Category not found");
            }
            return Ok(_mapper.Map<CategoryDto>(category));
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryData)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var categoryModel = _mapper.Map<Category>(categoryData);
            var result = await _categoryRepo.CreateAsync(categoryModel);

            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryModel.Id }, _mapper.Map<CategoryDto>(categoryModel));
        }

        [HttpPut("update/{Id:int}")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto updateData, [FromRoute] int Id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updateDto = _mapper.Map<Category>(updateData);
            var update = await _categoryRepo.UpdateAsync(updateDto, Id);

            if (update == null)
            {
                return NotFound("Category doesn't exist");
            }

            return Ok(_mapper.Map<CategoryDto>(update));
        }

        [HttpDelete("delete/{Id:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int Id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = await _categoryRepo.DeleteAsync(Id);

            if (category == null) return NotFound("Category not found");

            return NoContent();
        }
    }
}