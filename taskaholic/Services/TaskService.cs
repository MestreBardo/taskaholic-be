using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskaholic.DataBaseModels;
using taskaholic.DTOs;
using taskaholic.Models;
using taskaholic.Repository;

namespace taskaholic.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskRepository _taskRepository;
        private readonly IConfiguration _configuration;
        public TaskService(IUserDatabaseSettings settings, IConfiguration configuration)
        {
            _configuration = configuration;
            _taskRepository = new TaskRepository(settings);
        }

        public async Task<List<Tarefa>> CreateTask(TarefaDTO tarefa)
        {
            List<Tarefa> Novatarefas = await _taskRepository.CreateTask(tarefa);
            return Novatarefas;
        }
        public async Task<List<Tarefa>> GetTarefas(TarefaDTO tarefa)
        {
            List<Tarefa> Novatarefas = await _taskRepository.GetTarefas(tarefa);
            return Novatarefas;
        }
    }
}
