using GerenciamentoTarefasApi.Data.Dtos;
using GerenciamentoTarefasApi.Models;

namespace GerenciamentoTarefasApi.Services
{

    public interface ITarefaService
    {
        // Retorna todas as tarefas (com filtros opcionais).
        Task<List<Tarefa>> GetAllTasksAsync(string userId = null, TaskStatus? status = null);

        // Retorna uma tarefa pelo ID.
        Task<Tarefa> GetTaskByIdAsync(int id);

        // Cria uma nova tarefa.
        Task<Tarefa> CreateTaskAsync(CreateTarefaDto taskDto);

        // Atualiza uma tarefa existente.
        Task<Tarefa> UpdateTaskAsync(int id, UpdateTarefaDto taskDto);

        // Exclui uma tarefa.
        Task DeleteTaskAsync(int id);
    }

}
