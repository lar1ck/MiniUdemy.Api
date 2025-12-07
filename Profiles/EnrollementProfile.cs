using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MiniUdemy.Api.Dtos.Enrollment;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Profiles
{
    public class EnrollementProfile: Profile
    {
        public EnrollementProfile()
        {
            CreateMap<Enrollment, EnrollmentDto>()
                        .ForMember(des => des.Course, opt => opt.MapFrom(src => src.Course.Title))
                        .ForMember(des => des.User, opt => opt.MapFrom(src => src.Student.UserName));
            CreateMap<EnrollDto, Enrollment>();
            CreateMap<WithdrawDto, Enrollment>();
        }
    }
}