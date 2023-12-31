﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.Models;
using Models.ViewModel;
using Services.IServices;
using Services.Services;
using System.Security.Claims;

namespace Interface.Controllers
{
    [ApiController]
    [Route("Marber/BeerController")]
    public class BeerController : ControllerBase
    {
        private readonly IBeerService _beerService;
        private readonly ILogger<BeerController> _logger;

        public BeerController(IBeerService beerService, ILogger<BeerController> logger)
        {
            _beerService = beerService;
            _logger = logger;
        }

        
        [HttpGet("GetBeers")]
        public ActionResult<List<BeerDTO>> GetBeers()
        {
            try
            {
                if (_beerService.GetListBeer() != null)
                {
                    return Ok(_beerService.GetListBeer());
                }
                else
                {
                    throw new Exception();
                }                
            }
            catch (Exception exe)
            {
                _logger.LogError($"Ocurrio un error en el controlador GetBeers: {exe.Message}");
                return BadRequest($"Error al buscar la lista de todas las cervezas. Error: {exe.Message}");
            }
        }

        [HttpGet("GetBeerById")]
        public ActionResult<BeerDTO> GetBeerById(int id) 
        {
            try
            {
                if (_beerService.GetBeerById(id) != null)
                {
                    return Ok(_beerService.GetBeerById(id));
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception exe)
            {
                _logger.LogError($"Ocurrio un error en el controlador GetBeerById: {exe.Message}");
                return BadRequest($"Error al buscar una cerveza por su id. Error: {exe.Message}");
            }
        }

        [Authorize]
        [HttpPatch("ModifyPriceBeerById/{id}")]
        public ActionResult<string> ModifyPriceBeerById(int id, [FromBody] decimal newPrice)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "client")
            {
                return Forbid();
            }

            try
            {
                if (_beerService.ModifyPriceBeerById(id, newPrice) == true)
                {
                    return Ok("Cerveza modificada con exito");
                }
                else 
                {
                    throw new Exception();
                }
            }
            catch (Exception exe)
            {
                _logger.LogError($"Ocurrio un error en el controlador ModifyPriceBeerById: {exe.Message}");
                return BadRequest($"Error al modificar una cerveza. Error: {exe.Message}");
            }
        }

        [Authorize]
        [HttpPost("AddBeer")]
        public ActionResult<string> AddBeer([FromBody] AddBeerViewModel addBeerViewModel)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "client")
            {
                return Forbid();
            }

            try
            {
                var response = _beerService.AddBeer(addBeerViewModel);
                if (response != null)
                {
                    string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                    string apiAndEndpointUrl = $"Marber/BeerController/GetBeerById";
                    string locationUrl = $"{baseUrl}/{apiAndEndpointUrl}?id={response.Id}";
                    return Created(locationUrl, response);
                } 
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception exe)
            {
                _logger.LogError($"Ocurrio un error en el controlador AddBeer: {exe.Message}");
                return BadRequest($"Error al agregar una cerveza. Error: {exe.Message}");
            }
        }

        [Authorize]
        [HttpDelete("DeleteBeerById/{id}")]
        public ActionResult<string> DeleteBeerById(int id)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "client")
            {
                return Forbid();
            }

            try
            {
                if (_beerService.DeleteBeerById(id) == true)
                {
                    return Ok("Recurso eliminado con exito");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception exe)
            {
                _logger.LogError($"Ocurrio un error en el controlador DeleteBeerById: {exe.Message}");
                return BadRequest($"Ocurrio un error al intentar eliminar una cerveza. Error: {exe.Message}");
            }        
        }
    }
}
