using AutoMapper;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Models.Models;
using Models.ViewModel;
using Services.IServices;
using Services.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly Marber_BBDDContext _dbContext;
        private readonly IMapper _mapper;

        public OrderService(Marber_BBDDContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = AutoMapperConfig.Configure();
        }

        public List<OrderDTO> GetOrders()
        {
            throw new NotImplementedException();
        }

        public bool AddOrder(int id, List<OrderViewModel> orders, int userId)
        {
            try
            {
                foreach (var beer in orders)
                {
                    _dbContext.Orders.Add(new Orders
                    {
                        Id = id,
                        IdUser = userId,
                        IdBeer = beer.Id,   
                        Quantity = beer.Quantity,
                        SubTotal = beer.Price
                    });
                }
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception exe)
            {
                string error = exe.Message;
                return false;
            }
        }


        //    public (int idOrder, List<int> products, int userId)
        //    {

        //        foreach (var item in products){
        //        contexto.Order.Add(new Orders{
        //            id = idOrder,
        //            idprod = item.idprod,
        //            qu = item.quantit,
        //            subt = item.price
        //})
        //        }

        //    }    
    }
}
