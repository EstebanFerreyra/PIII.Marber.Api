using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.Models;
using Services.IServices;
using System.Security.Claims;

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

        [HttpGet("topBeers")]
        public ActionResult<List<SalesDTO>> GetTopSoldProducts()
        {
            var topSoldBeers = _dbContext.Orders
                .GroupBy(o => o.IdBeer)
                .Select(g => new
                {
                    TotalSold = g.Sum(o => o.Quantity)
                })
                .OrderByDescending(p => p.TotalSold)
                .Take(3)
                .Join(_dbContext.Orders, p => p.TotalSold, pr => pr.IdBeer, (p, pr) => pr)
                .ToList();

            return Ok(topSoldBeers);
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
