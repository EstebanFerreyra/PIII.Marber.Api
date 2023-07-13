using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ILogger<SalesController> _logger;
        private readonly ISalesService _salesService;

        public SalesController(ILogger<SalesController> logger, ISalesService salesService)
        {
            _logger = logger;
            _salesService = salesService;
        }

        [HttpGet("topBeers")]
        public ActionResult<List<BeerDTO>> GetTopSoldProducts()
        {
            try
            {
                var response = _salesService.GetTopSoldProducts();
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception exe)
            {
                _logger.LogError($"Ocurrio un error en el controlador SalesController: {exe.Message}");
                return BadRequest($"Error al intentar ver las cervezas mas compradas. Error: {exe.Message}");
            }
        }

        [Authorize]
        [HttpGet("topBuyers")]
        public ActionResult<List<UserDTO>> GetBuyers()
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value != "superadmin")
            {
                return Forbid();
            }

            try
            {
                var response = _salesService.GetBuyers();
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception exe)
            {
                _logger.LogError($"Ocurrio un error en el controlador SalesController: {exe.Message}");
                return BadRequest($"Error al intentar ver compradores con mas compras. Error: {exe.Message}");
            }
        }
    }
}
