using AutoMapper;
using Services.Mappings.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mappings
{
    public static class AutoMapperConfig
    {
        public static IMapper Configure()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BeerProfile>();
                cfg.AddProfile<UserProfile>();
                //cfg.AddProfile<UserProfile>();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            return mapper;
        }
    }
}
