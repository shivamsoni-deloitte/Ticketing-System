using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tickeing_system.models;
using tickeing_system.Services;

namespace tickeing_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        IUserService _userService;

        public UserController(IUserService service){
            _userService = service;
        }

        // Controller to get all Users
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllUsers() {
            try {
                var users = _userService.GetUserList();
                if (users == null) return NotFound();
                return Ok(users);
            } catch (Exception) {
                return BadRequest();
            }
        }

        // Controller to save User
        [HttpPost]
        [Route("[action]")]
        public IActionResult SaveUser(User userModel) {
            try {
                var model = _userService.SaveUser(userModel);
                return Ok(model);
            } catch (Exception) {
                return BadRequest();
            }
        }
    }
}