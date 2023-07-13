using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using Models.Models;
using Models.ViewModel;
using Services.IServices;
using Services.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class BeerService : IBeerService
    {
        private readonly Marber_BBDDContext _dbContext;
        private readonly IMapper _mapper;

        public BeerService(Marber_BBDDContext context)
        {
            _dbContext = context;
            _mapper = AutoMapperConfig.Configure();
        }

        public List<BeerDTO> GetListBeer()
        {
            try
            {
                return _mapper.Map<List<BeerDTO>>(_dbContext.Beer.ToList());
            }
            catch (Exception exe)
            {
                string error = exe.Message;
                return null;
            }
        }

        public BeerDTO GetBeerById(int id)
        {
            try
            {
                return _mapper.Map<BeerDTO>(_dbContext.Beer.Where(w => id == w.Id).FirstOrDefault());
            }
            catch (Exception exe)
            {
                string error = exe.Message;
                return null;
            }            
        }

        public bool ModifyPriceBeerById(int id, decimal newPrice)
        {
            try
            {
                if (_dbContext.Beer.FirstOrDefault(f => f.Id == id) == null)
                {
                    return false;
                }

                foreach (Beer beer in _dbContext.Beer.ToList())
                {
                    if (beer.Id == id)
                    {
                        beer.Price = newPrice;
                    }
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

        public BeerDTO AddBeer(AddBeerViewModel addBeerViewModel)
        {
            try
            {
                _dbContext.Beer.Add(_mapper.Map<Beer>(addBeerViewModel));
                _dbContext.SaveChanges();
                return _mapper.Map<BeerDTO>(_dbContext.Beer.OrderBy(o => o.Id).Last());
            }
            catch (Exception exe)
            {
                string error = exe.Message;
                return null;
            }
        }

        public bool DeleteBeerById(int id)
        {
            try
            {
                Beer beer = _dbContext.Beer.Where(w => w.Id == id).FirstOrDefault();
                if (beer != null)
                {
                    _dbContext.Beer.Remove(beer);
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
