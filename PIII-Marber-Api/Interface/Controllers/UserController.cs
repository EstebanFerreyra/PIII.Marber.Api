using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services.Services;

namespace Interface.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }


        [HttpGet("GetUsers")]
        public ActionResult<List<Users>> GetUsers()
        {
            var response = _userService.GetListUser();
            return Ok(response);
        }
    }
}
