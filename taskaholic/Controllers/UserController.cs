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
                return BadRequest("Usuário não cadastrado");
            var token = _authService.GenerateToken(userLog);
            return Ok(new
            {
                token
            });
        }
        [HttpPost("ChangeActive")]
        public async Task<IActionResult> ChangeActive(ChangeActiveDTO change)
        {
            bool response = await _authService.ChangeUserActive(change.Id, change.isActive);
            if (response)
            {
                return Ok(response);
            }
            return BadRequest("Não foi possivel atualizar o usuário");
         
        }
        [HttpPost("ChangeData")]
        public async Task<IActionResult> ChangeUserData(UserChangeDTO change)
        {
            if (change.Password != null && change.ReinputPassword != null)
            {
                if (!_authService.CheckPassword(change.Password, change.ReinputPassword))
                {
                    return BadRequest("As senhas não são iguais.");
                };
            }
            bool response = await _authService.ChangeUserData(change);
            if (response)
            {
                return Ok(response);
            }
            return BadRequest("Não foi possivel atualizar o usuário");

        }
        [HttpPost("Usuarios")]
        public async Task<ActionResult> GetUsers(SearchDTO search)
        {
            List<User> users = await _authService.GetUsers(search.SearchCriteriaEmail);
            if (users.Count() == 0)
                return BadRequest("Usuário não cadastrado");
            return Ok(users);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> CreateUser(UserRegisterDTO urd) {
            try
            {
                if (!_authService.CheckPassword(urd.Password, urd.ReinputPassword)) {
                    return BadRequest("As senhas não são iguais.");
                };
                if (!_authService.CheckRole(urd.Role))
                {
                    return BadRequest("O Permissionamento informado não existe.");
                }
                User novoUser = new User(urd.Name, urd.Email, urd.Role, new List<Tarefa>(),urd.isActive);

                User user = await _authService.CreateUser(novoUser, urd.Password);
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
