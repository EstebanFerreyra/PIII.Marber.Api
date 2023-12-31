﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.Models;
using Models.ViewModel;
using Services.IServices;
using Services.Services;
using System.Security.Claims;
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

        [Authorize]
        [HttpGet("GetUsers")]
        public ActionResult<List<UserDTO>> GetUsers()
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "client")
            {
                return Forbid();
            }

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

        [Authorize]
        [HttpGet("GetUserById/{id}")]
        public ActionResult<UserDTO> GetUserById(int id)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "client")
            {
                return Forbid();
            }

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
                var response = _service.AddUser(user);
                if (response != null)
                {
                    string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                    string apiAndEndpointUrl = $"interface/Users/GetUserById";
                    string localtionUrl = $"{baseUrl}/{apiAndEndpointUrl}/{response.Id}";
                    return Created(localtionUrl, response);
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

        [Authorize]
        [HttpPut("UpdateUser")]
        public ActionResult<UserDTO> UpdateUser([FromBody] ModifyUserViewModel user)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value != "superadmin")
            {
                return Forbid();
            }

            try
            {
                var response = _service.UpdateUser(user);
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
                _logger.LogError($"Ocurrio un error en el controlador UpdateUser {exe.Message}");
                return BadRequest($"Error al intentar modificar un usuario. Error {exe.Message}");
            }
        }

        [Authorize]
        [HttpPatch("ChangeUsername")]
        public ActionResult<string> ModifyUserName(int id, [FromBody] string newUserName) 
        {
            try
            {
                if (_service.ModifyUserName(id, newUserName) == true)
                {
                    return Ok("Usuario modificado con éxito.");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception exe)
            {
                _logger.LogError($"Ocurrio un error en el controlador ModifyUserName: {exe.Message}");
                return BadRequest($"Error al modificar datos del usuario. Error: {exe.Message}");
            }
        }

        [Authorize]
        [HttpPatch("ChangePassword")]
        public ActionResult<string> ModifyPassword(int id, [FromBody] string newPassword)
        {
            try
            {
                if (_service.ModifyPassword(id, newPassword) == true)
                {
                    return Ok("Contraseña modificada con éxito.");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception exe)
            {
                _logger.LogError($"Ocurrió un error en el controlador ModifyPassword: {exe.Message}");
                return BadRequest($"Error al modificar datos del usuario. Error: {exe.Message}");
            }
        }

        [Authorize]
        [HttpDelete("DeleteUser/{id}")]
        public ActionResult<string> DeleteUser(int id)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value != "superadmin")
            {
                return Forbid();
            }

            try
            {
                if (_service.DeleteUser(id) == true)
                {
                    return Ok("Usuario eliminado con éxito.");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrió un error en el controlador DeleteUser: {ex.Message}");
                return BadRequest($"Error al intentar borrar usuario con id {id}. Error: {ex.Message}");
            }
        }
    }
}
