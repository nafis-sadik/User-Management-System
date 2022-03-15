using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController()
        {
            _userService = new UserServices();
        }
        [Route("LogIn")]
        [HttpGet]
        public IActionResult LogIn(string userName, string password)
        {
            return Ok(_userService.AuthenticateUser(userName, password));
        }
        [Route("SignUp")]
        [HttpPost]
        public IActionResult SignUp(UserModel userModel)
        {
            return Conflict(_userService.RegisterUser(userModel));
        }
    }
}
