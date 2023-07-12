using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
            var ordersList = new List<OrderDTO>();
            foreach (var order in _dbContext.Orders.ToList()) {
                ordersList.Add(new OrderDTO
                {
                    Id = order.Id,
                    UserName = _dbContext.Users.Where(w => w.Id == order.IdUser).FirstOrDefault().UserName,
                    BeerName = _dbContext.Beer.Where(w => w.Id == order.IdBeer).FirstOrDefault().BeerName,
                    Quantity = order.Quantity,
                    SubTotal = order.SubTotal * order.Quantity,
                }); 
                };
            return ordersList;
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

        public bool DeleteOrderById(int id)
        {
            try
            {
                var listToDelete = new List<Orders>();
                listToDelete = _dbContext.Orders.Where(w => w.Id == id).ToList();
                if (listToDelete.Count > 0)
                {
                    foreach (var order in listToDelete)
                    {
                        _dbContext.Orders.Remove(order);
                    }
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception exe)
            {
                string error = exe.Message;
                return false;
            }
        }
    }
}
