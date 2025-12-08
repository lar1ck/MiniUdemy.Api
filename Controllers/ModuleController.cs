using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api1.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;
        public ModuleController(
            IModuleRepository moduleRepo, 
            IMapper mapper, 
            UserManager<AppUser> userManager)
        {
            _moduleRepo = moduleRepo;
            _mapper = mapper;
            _userManager = userManager;
            
        }

        [HttpGet("all")]
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> GetAllModules()
        {
            var modules = await _moduleRepo.GetAllAsync();
            return Ok(_mapper.Map<List<ModuleDto>>(modules));
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = ("Admin, Tutor"))]
        public async Task<IActionResult> GetModule([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var module = await _moduleRepo.GetByIdAsync(id);

            if (module == null) return NotFound("Module doesn't exist");

            return Ok(_mapper.Map<ModuleDto>(module));
        }

        [HttpGet]
        [Authorize(Roles = ("Student"))]
        public async Task<IActionResult> GetUserModule()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userName = User.Getusername();
            var appUser = await _userManager.FindByNameAsync(userName);
            var module = await _moduleRepo.GetUserModulesAsync(appUser);

            return Ok(_mapper.Map<List<ModuleDto>>(module));
        }

        [HttpPost("create")]
        [Authorize( Roles = ("Tutor"))]
        public async Task<IActionResult> CreateModule([FromBody] CreateModuleDto data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var moduleData = _mapper.Map<CModule>(data);
            var result = await _moduleRepo.CreateAsync(moduleData);

            var newModule = await _moduleRepo.GetByIdAsync(result.Id);

            return CreatedAtAction(nameof(GetModule), new { id = result.Id }, _mapper.Map<ModuleDto>(newModule));
        }

        [HttpPut("update/{id:int}")]
        [Authorize( Roles = ("Tutor"))]
        public async Task<IActionResult> UpdateModule([FromBody] UpdateModuleDto data, [FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userName = User.Getusername();
            var appUser = await _userManager.FindByNameAsync(userName);

            var moduleData = _mapper.Map<CModule>(data);
            var result = await _moduleRepo.UpadteAsync(moduleData, id, appUser);

            if (result == null) return NotFound("Module doesn't exist");

            var newModule = await _moduleRepo.GetByIdAsync(result.Id);

            return Ok(_mapper.Map<ModuleDto>(newModule));
        }

        [HttpDelete("delete/{id:int}")]
        [Authorize( Roles = ("Tutor"))] // make suer it is the owner
        public async Task<IActionResult> DeleteModule([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _moduleRepo.DeleteAsync(id);

            if (result == null) return NotFound("Module doesn't exist");

            return NoContent();
        }
    }
}