using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.Models;
using Models.ViewModel;
using Services.IServices;
using System.Security.Claims;

namespace Interface.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService service, ILogger<OrdersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetOrders")]
        public ActionResult<List<OrderDTO>> GetOrders()
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "client")
            {
                return Forbid();
            }

            try
            {
                var response = _service.GetOrders();
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
                _logger.LogError($"Ocurrio un error en el controlador OrdersController: {exe.Message}");
                return BadRequest($"Error al mostrar ordenes. Error: {exe.Message}");
            }
        }

        [HttpPost("AddOrder")]
        public ActionResult<string> AddOrder(int id, [FromBody] List<OrderViewModel> beers) 
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            try
            {
                if (_service.AddOrder(id, beers, Int32.Parse(userId)))
                {
                    return Ok("Orden agregada con exito");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception exe)
            {
                _logger.LogError($"Ocurrio un error en el controlador OrdersController: {exe.Message}");
                return BadRequest($"Error al agregar ordenes. Error: {exe.Message}");
            }
        }

        [HttpDelete("DeleteOrder/{id}")]
        public ActionResult<string> DeleteOrder(int id)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "client")
            {
                return Forbid();
            }

            try
            {
                if (_service.DeleteOrderById(id) == true)
                {
                    return Ok("Orden eliminada con exito");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception exe)
            {
                _logger.LogError($"Ocurrio un error en el controlador OrdersController: {exe.Message}");
                return BadRequest($"Error al eliminar ordenes. Error: {exe.Message}");
            }
        }
    }
}
