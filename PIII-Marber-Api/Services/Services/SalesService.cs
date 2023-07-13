using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Models.Models;
using Services.IServices;
using Services.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class SalesService : ISalesService
    {
        private readonly Marber_BBDDContext _dbContext;
        private readonly IMapper _mapper;

        public SalesService(Marber_BBDDContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = AutoMapperConfig.Configure();
        }

        public List<BeerDTO> GetTopSoldProducts()
        {
            try
            {
                var listProductAndQuantity = new List<IdProductsAndQuantitySoldDTO>();

                foreach (var order in _dbContext.Orders.ToList())
                {
                    if (listProductAndQuantity.Where(w => w.IdBeer == order.IdBeer).FirstOrDefault() == null)
                    {
                        listProductAndQuantity.Add(new IdProductsAndQuantitySoldDTO
                        {
                            IdBeer = order.IdBeer,
                            Quantity = order.Quantity
                        });
                    }
                    else
                    {
                        foreach (var prod in listProductAndQuantity)
                        {
                            if (prod.IdBeer == order.IdBeer)
                            {
                                prod.Quantity += order.Quantity;
                            }
                        }
                    }
                }

                var topBeers = listProductAndQuantity
                    .OrderByDescending(q => q.Quantity)
                    .Take(3)
                    .ToList();

                var list = new List<BeerDTO>();

                foreach (var prod in topBeers)
                {
                    list.Add(_mapper.Map<BeerDTO>(_dbContext.Beer.Where(w => w.Id == prod.IdBeer).FirstOrDefault()));
                }

                return list;
            }
            catch (Exception exe)
            {
                string error = exe.Message;
                return null;
            }
        }

        public List<UserDTO> GetBuyers()
        {
            try
            {
                var listUserAndQuantity = new List<IdUserAndQuanitySoldDTO>();

                foreach (var order in _dbContext.Orders.ToList())
                {
                    if (listUserAndQuantity.Where(w => w.IdUser == order.IdUser).FirstOrDefault() == null)
                    {
                        listUserAndQuantity.Add(new IdUserAndQuanitySoldDTO
                        {
                            IdUser = order.IdUser,
                            Quantity = order.Quantity
                        });
                    }
                    else
                    {
                        foreach (var prod in listUserAndQuantity)
                        {
                            if (prod.IdUser == order.IdUser)
                            {
                                prod.Quantity += order.Quantity;
                            }
                        }
                    }
                }

                var topBuyers = listUserAndQuantity
                    .OrderByDescending(q => q.Quantity)
                    .Take(3)
                    .ToList();

                var list = new List<UserDTO>();

                foreach (var buyer in topBuyers)
                {
                    list.Add(_mapper.Map<UserDTO>(_dbContext.Users.Where(w => w.Id == buyer.IdUser).FirstOrDefault()));
                }
                return list;
            }
            catch (Exception exe)
            {
                string error = exe.Message;
                return null;
            }
        }
    }
}
