using AutoMapper;
using Models.DTO;
using Models.Models;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mappings.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        { 
            CreateMap<Users, UserDTO>()
                .ForMember(dest => dest.IdRole, opt=> opt.MapFrom(src => src.IdRole))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.UserEmail))
                .ForMember(dest => dest.UserPassword, opt => opt.MapFrom(src=> src.UserPassword));

            CreateMap<UserDTO, Users>();

            CreateMap<List<Users>, List<UserDTO>>()
                .ConvertUsing(src => src.Select(u => new UserDTO 
                { 
                    IdRole= u.IdRole,
                    UserName = u.UserName,
                    UserEmail = u.UserEmail,
                    UserPassword = u.UserPassword,
                    Id = u.Id })
                .ToList());

            CreateMap<UserViewModel, Users>();

            CreateMap<UserViewModel, UserDTO>()
                .ForMember(dest => dest.IdRole, opt => opt.MapFrom(src => src.IdRole))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.UserEmail))
                .ForMember(dest => dest.UserPassword, opt => opt.MapFrom(src => src.UserPassword));
        }
    }
}
