using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.Models;
using Models.ViewModel;
using Services.IServices;
using Services.Services;
using System.Security.Cryptography.X509Certificates;

namespace Interface.Controllers
{
    [Route("Marber/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService service, ILogger<UserController> logger)
        {
            _service = service;
            _logger = logger;
        }


        [HttpGet("GetUsers")]
        public ActionResult<List<UserDTO>> GetUsers()
        {
            try
            {
                if (_service.GetUsers() != null)
                {
                    return Ok(_service.GetUsers());
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error en el controlador GetUsers: {ex.Message}");
                return BadRequest($"Error al intentar eliminar usuario. Error: {ex.Message}");
            }
        }

        [HttpGet("GetUserById/{id}")]
        public ActionResult<UserDTO> GetUserById(int id)
        {
            try
            {
                if (_service.GetUserById(id) != null)
                {
                    return Ok(_service.GetUserById(id));
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el controlador GetUserById: {ex.Message}");
                return BadRequest($"Error al buscar usuario con id {id}. Error: {ex.Message}");
            }
        }

        [HttpPost("AddUser")]
        public ActionResult<UserDTO> AddUser([FromBody] UserViewModel user)
        {
            try
            {
                if (_service.AddUser(user) != null)
                {
                    string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                    string apiAndEndpointUrl = $"interface/Users/GetUserById";
                    string localtionUrl = $"{baseUrl}/{apiAndEndpointUrl}/{_service.AddUser(user).Id}";
                    return Created(localtionUrl, _service.AddUser(user));
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en el controlador AddUser: {ex.Message}");
                return BadRequest($"Error al intentar agregar usuario. Error: {ex.Message}");
            }
        }

        [HttpPut("UpdateUser")]
        public ActionResult<UserDTO> UpdateUser([FromBody] UserViewModel user)
        {
            try
            {
                if (_service.UpdateUser(user) != null)
                {
                    string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                    string apiAndEndPointUrl = $"interface/Users/GetUserById";
                    string locationUrl = $"{baseUrl}/{apiAndEndPointUrl}/{_service.UpdateUser(user).Id}";
                    return Created(locationUrl, _service.UpdateUser(user));
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception exe)
            {
                _logger.LogError($"Ocurrio un error en el controlador UpdateUser {exe.Message}");
                return BadRequest($"Error al intentar modificar un usuario. Error {exe.Message}");
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        public ActionResult<string> DeleteUser(int id)
        {
            try
            {
                if (_service.DeleteUser(id) == true)
                {
                    return Ok();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en el controlador DeleteUser: {ex.Message}");
                return BadRequest($"Error al intentar borrar un usuario con id {id}. Error: {ex.Message}");
            }
        }
    }
}
