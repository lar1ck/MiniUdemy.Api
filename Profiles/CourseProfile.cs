using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MiniUdemy.Api.Dtos.Course;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Profiles
{
    public class CourseProfile: Profile
    {
        public CourseProfile()
        {
            CreateMap<CreateCourseDto, Course>();
            CreateMap<Course, CourseDto>();
            CreateMap<Course, DisplayCourseDto>()
                .ForMember(des => des.Instructor, opt => opt.MapFrom(src => src.Instructor.UserName))
                .ForMember(des => des.Category, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}