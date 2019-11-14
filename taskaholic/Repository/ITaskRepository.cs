using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskaholic.DTOs;
using taskaholic.Models;

namespace taskaholic.Repository
{
    public interface ITaskRepository
    {
        Task<List<Tarefa>> CreateTask(TarefaDTO tarefa);
        Task<List<Tarefa>> GetTarefas(TarefaDTO tarefa);
    }
}
