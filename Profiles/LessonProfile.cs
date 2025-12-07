using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MiniUdemy.Api.Dtos.Lesson;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Profiles
{
    public class LessonProfile: Profile
    {
        public LessonProfile()
        {
            CreateMap<Lesson, LessonDto>()
                .ForMember(des => des.Module, opt => opt.MapFrom(src => src.Module.Title));
            CreateMap<CreateLessonDto, Lesson>();
            CreateMap<UpdateLessonDto, Lesson>();
        }
    }
}