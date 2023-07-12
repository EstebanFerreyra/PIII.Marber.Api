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
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // ACA HAY QUE VER SI CREAMOS EL PERFIL O EN EL SERVICE HACEMOS LOS JOINS
            //CreateMap<Orders, OrderDTO>()
            //    .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            //    .ForMember(dest => dest.SubTotal, opt => opt.MapFrom(src => src.SubTotal));


            //CreateMap<UserViewModel, UserDTO>()
            //    .ForMember(dest => dest.IdRole, opt => opt.MapFrom(src => src.IdRole))
            //    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            //    .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.UserEmail))
            //    .ForMember(dest => dest.UserPassword, opt => opt.MapFrom(src => src.UserPassword));

            //CreateMap<List<Beer>, List<BeerDTO>>()
            //    .ConvertUsing(src => src.Select(b => new BeerDTO
            //    {
            //        Id = b.Id,
            //        BeerName = b.BeerName,
            //        BeerStyle = b.BeerStyle,
            //        Price = b.Price
            //    }).ToList());
        }
    }
}
