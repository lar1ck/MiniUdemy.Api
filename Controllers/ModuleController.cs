using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MiniUdemy.Api.Dtos.Module;
using MiniUdemy.Api.Interface;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Controllers
{
    [Route("api/module")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleRepository _moduleRepo;
        private readonly IMapper _mapper;
        public ModuleController(IModuleRepository moduleRepo, IMapper mapper)
        {
            _moduleRepo = moduleRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllModules()
        {
            var modules = await _moduleRepo.GetAllAsync();
            return Ok(_mapper.Map<List<ModuleDto>>(modules));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetModule([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var module = await _moduleRepo.GetByIdAsync(id);

            if (module == null) return NotFound("Module doesn't exist");

            return Ok(_mapper.Map<ModuleDto>(module));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateModule([FromBody] CreateModuleDto data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var moduleData = _mapper.Map<CModule>(data);
            var result = await _moduleRepo.CreateAsync(moduleData);

            var newModule = await _moduleRepo.GetByIdAsync(result.Id);

            return CreatedAtAction(nameof(GetModule), new { id = result.Id }, _mapper.Map<ModuleDto>(newModule));
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateModule([FromBody] UpdateModuleDto data, [FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var moduleData = _mapper.Map<CModule>(data);
            var result = await _moduleRepo.UpadteAsync(moduleData, id);

            if (result == null) return NotFound("Module doesn't exist");

            var newModule = await _moduleRepo.GetByIdAsync(result.Id);

            return Ok(_mapper.Map<ModuleDto>(newModule));
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteModule([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _moduleRepo.DeleteAsync(id);

            if (result == null) return NotFound("Module doesn't exist");

            return NoContent();
        }
    }
}