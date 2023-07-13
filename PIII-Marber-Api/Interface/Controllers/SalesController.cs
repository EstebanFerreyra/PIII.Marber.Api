using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using Models.Models;
using Services.IServices;
using Services.Mappings;
using System.Security.Claims;

namespace Interface.Controllers
{
    [ApiController]
    [Route("Marber/SalesController")]
    public class SalesController : ControllerBase
    {
        private readonly Marber_BBDDContext _dbContext;
        private readonly ILogger<SalesController> _logger;
        private readonly IMapper _mapper;

        public SalesController(Marber_BBDDContext _context, ILogger<SalesController> logger)
        {
            _dbContext = _context;
            _logger = logger;
            _mapper = AutoMapperConfig.Configure();
        }

        [HttpGet("topBeers")]
        public ActionResult<List<BeerDTO>> GetTopSoldProducts()
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

            return Ok(list);
        }

        [HttpGet("topBuyers")]
        public ActionResult<List<UserDTO>> GetBuyers()
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value != "superadmin")
            {
                return Forbid();
            }

            try
            {
                if (_dbContext.Orders != null)
                {
                    var usualClients = _dbContext.Orders
                        .GroupBy(o => o.IdUser)
                        .Select(g => new
                        {
                            TotalBuys = g.Sum(o => o.Quantity)
                        })
                        .OrderByDescending(p => p.TotalBuys)
                        .Take(3)
                        .Join(_dbContext.Orders, p => p.TotalBuys, pr => pr.IdUser, (p, pr) => pr)
                        .ToList();

                    return Ok(usualClients); 
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en el controlador TopSales: {ex.Message}");
                return BadRequest($"Error al intentar mostrar compradores frecuentes. Error: {ex.Message}");
            }
        }
    }
}
