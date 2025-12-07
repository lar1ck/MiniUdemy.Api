using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MiniUdemy.Api.Dtos.Review;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Profiles
{
    public class ReviewProfile: Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDto>()
                        .ForMember(des => des.Course, opt => opt.MapFrom(src => src.Course.Title))
                        .ForMember(des => des.User, opt => opt.MapFrom(src => src.User.UserName));
            CreateMap<CreateReviewDto, Review>();
        }
    }
}