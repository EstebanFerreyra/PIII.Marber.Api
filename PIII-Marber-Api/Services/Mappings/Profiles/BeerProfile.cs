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
    public class BeerProfile : Profile
    {
        public BeerProfile()
        {
            CreateMap<Beer, BeerDTO>();
            CreateMap<BeerDTO, Beer>();
            CreateMap<Beer, AddBeerViewModel>();
            CreateMap<AddBeerViewModel, Beer>();
            CreateMap<List<Beer>, List<BeerDTO>>()
                .ConvertUsing(src => src.Select(b => new BeerDTO 
                {   
                    Id = b.Id, 
                    BeerName = b.BeerName, 
                    BeerStyle = b.BeerStyle, 
                    Price = b.Price 
                }).ToList());
        }
    }
}
//CreateMap<List<Empleado>, List<EmpleadoDTO>>()
//              .ConvertUsing(src => src.Select(e => new EmpleadoDTO { NombreEmpleado = e.Nombre, Id = e.Id }).ToList());
