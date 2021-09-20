using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.User.Queries.Dto;

namespace UseCases.User.Queries.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Entities.Models.User, UserDto>()
                .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.UserName))
                .ForMember(dest => dest.IsActive, act => act.MapFrom(src => src.NotificationContext.IsActive));
        }
    }
}
