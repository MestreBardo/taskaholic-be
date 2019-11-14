
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using taskaholic.DTOs;
using taskaholic.Models;
using taskaholic.Services;

namespace taskaholic.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IConfiguration _configuration;

        public TarefasController(ITaskService taskservice, IConfiguration configuration)
        {
            _taskService = taskservice;
            _configuration = configuration;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> ChangeUserData(TarefaDTO tarefas)
        {
            List<Tarefa> Novastarefas = await _taskService.CreateTask(tarefas);
            return Ok(Novastarefas);

        }
        [HttpPost("GetTarefas")]
        public async Task<IActionResult> GetTarefas(TarefaDTO tarefas)
        {
            List<Tarefa> Novastarefas = await _taskService.GetTarefas(tarefas);
            return Ok(Novastarefas);

        }
    }
}