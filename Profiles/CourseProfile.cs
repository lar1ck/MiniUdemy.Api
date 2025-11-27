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
            CreateMap<Course, CreateCourseDto>();
        }
    }
}