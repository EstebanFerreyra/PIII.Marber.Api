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
            // Acá se mappearía de una entidad a otra 
            // de Orders a Users por ejemplo
        }
    }
}
