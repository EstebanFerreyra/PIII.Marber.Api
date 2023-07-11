using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.Models;
using Services.IServices;

namespace Interface.Controllers
{
    [ApiController]
    [Route("Marber/SalesController")]
    public class SalesController : ControllerBase
    {
        private readonly Marber_BBDDContext _dbContext;
        private readonly ILogger<SalesController> _logger;

        public SalesController(Marber_BBDDContext _context, ILogger<SalesController> logger)
        {
            _dbContext = _context;
            _logger = logger;
        }

        [HttpGet("topSoldBeers")]
        public ActionResult<List<SalesDTO>> GetTopSoldProducts()
        {
            var topSoldProducts = _dbContext.Orders
                .GroupBy(o => o.IdBeer)
                .Select(g => new
                {
                    TotalQuantitySold = g.Sum(o => o.Quantity)
                })
                .OrderByDescending(p => p.TotalQuantitySold)
                .Take(3)
                .Join(_dbContext.Orders, p => p.TotalQuantitySold, pr => pr.IdBeer, (p, pr) => pr)
                .ToList();

            return Ok(topSoldProducts);
        }

        [HttpGet("topBuyers")]
        public ActionResult<List<UserDTO>> GetBuyers()
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
    }
}
