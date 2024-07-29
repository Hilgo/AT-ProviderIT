using GerenciamentoTarefasApi.Data.Dtos;
using GerenciamentoTarefasApi.Models;

namespace GerenciamentoTarefasApi.Services
{

    public interface ITarefaService
    {
        // Retorna todas as tarefas (com filtros opcionais).
        public Task<List<Tarefa>> GetAllTasksAsync(string userId = null, StatusTarefa? status = null);

        // Retorna uma tarefa pelo ID.
        public Task<Tarefa> GetTaskByIdAsync(int id);

        // Cria uma nova tarefa.
        public Task<Tarefa> CreateTaskAsync(CreateTarefaDto taskDto);

        // Atualiza uma tarefa existente.
        public Task<Tarefa> UpdateTaskAsync(int id, UpdateTarefaDto taskDto);

        // Exclui uma tarefa.
        public Task DeleteTaskAsync(int id);
    }

}
