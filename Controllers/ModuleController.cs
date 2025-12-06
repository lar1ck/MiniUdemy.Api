using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MiniUdemy.Api.Dtos.Module;
using MiniUdemy.Api.Interface;

namespace MiniUdemy.Api.Controllers
{
    [Route("api/module")]
    [ApiController]
    public class ModuleController: ControllerBase
    {
        private readonly IModuleRepository _moduleRepo;
        private readonly IMapper _mapper;
        public ModuleController(IModuleRepository moduleRepo, IMapper mapper)
        {
            _moduleRepo = moduleRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetModules()
        {
            var modules = await _moduleRepo.GetAllAsync();
            return Ok(_mapper.Map<List<ModuleDto>>(modules));
        }
    }
}