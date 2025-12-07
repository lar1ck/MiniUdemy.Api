using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MiniUdemy.Api.Dtos.LessonProgress;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Profiles
{
    public class LessonProgressProfile: Profile
    {
        public LessonProgressProfile()
        {
            CreateMap<LessonProgress, LessonProgressDto>()
                        .ForMember(des => des.Lesson, opt => opt.MapFrom(src => src.Lesson.Title))
                        .ForMember(des => des.User, opt => opt.MapFrom(src => src.Student.UserName));
            CreateMap<CreateLessonProgressDto, LessonProgressDto>();
            CreateMap<CreateLessonProgressDto, LessonProgress>();
        }
    }
}