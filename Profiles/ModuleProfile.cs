using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MiniUdemy.Api.Dtos.Module;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Profiles
{
    public class ModuleProfile: Profile
    {
        public ModuleProfile()
        {
            CreateMap<CModule, ModuleDto>()
                .ForMember(des => des.Course, opt => opt.MapFrom(src => src.Course.Title));
        }
    }
}