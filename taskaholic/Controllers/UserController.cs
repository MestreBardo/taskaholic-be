using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using taskaholic.DTOs;
using taskaholic.Models;
using taskaholic.Services;

namespace taskaholic.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public UserController(IAuthService authservice, IConfiguration configuration)
        {
            _authService = authservice;
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> GetUser(UserLoginDTO loginDTO)
        {
            User userLog = await _authService.GetUser(loginDTO.Email, loginDTO.Password);
            if (userLog == null)
                Unauthorized();
            var token = _authService.GenerateToken(userLog);
            return Ok(new
            {
                token
            });
        }


        [HttpPost("Register")]
        public async Task<IActionResult> CreateUser(UserRegisterDTO urd) {
            try
            {
                urd.User.Assignments = new List<Assignment>();

                User user = await _authService.CreateUser(urd.User, urd.Password);
                if (user == null)
                {
                    return BadRequest("Usuário já existe");
                }
                return StatusCode(201);
            }
            catch (MongoWriteException mwe)
            {
                throw new Exception(mwe.GetType().Name);
            }
            catch (Exception e)
            {
                throw new Exception(e.GetType().Name);
            }

        }

    }
}
