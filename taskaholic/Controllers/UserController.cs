using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using taskaholic.Models;
using taskaholic.Services;

namespace taskaholic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userservice)
        {
            _userService = userservice;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _userService.Get());
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user) {
            try
            {
                user.Assignments = new List<Assignment>();
                await _userService.Create(user);
                return Ok(true);
            }
            catch (MongoWriteException mwe)
            {

            }
            catch (Exception e)
            {
                throw new Exception(e.GetType().Name);
            }

        }

    }
}
