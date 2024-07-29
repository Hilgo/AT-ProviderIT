using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GerenciamentoTarefasApi.Models;
using GerenciamentoTarefasApi.Data.Dtos;
using GerenciamentoTarefasApi.Services;

namespace GerenciamentoTarefasApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaController : Controller
    {
        private readonly ITarefaService _taskService;

        public TarefaController(ITarefaService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTasks()
        {
            // Verifica se o usuário é administrador
            var isAdmin = HttpContext.User.IsInRole("Admin");

            // Se for administrador, busca todas as tarefas
            if (isAdmin)
            {
                var tasks = await _taskService.GetAllTasksAsync();
                return Ok(tasks);
            }
            else
            {
                // Se não for administrador, busca apenas as tarefas do usuário logado
                var userId = GetCurrentUserId();
                var tasks = await _taskService.GetAllTasksAsync(userId);
                return Ok(tasks);
            }
        }

        // GET: api/tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> GetTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            // Verifica se o usuário é administrador ou se é o responsável pela tarefa
            if (IsAdminOrTaskOwner(task))
            {
                return Ok(task);
            }
            else
            {
                return Forbid();
            }
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<Tarefa>> CreateTask(CreateTarefaDto taskDto)
        {
            // Obtém o ID do usuário logado
            var userId = GetCurrentUserId();

            // Define o usuário logado como o responsável pela tarefa
            taskDto.UsuarioAssocioadoId = userId;

            var createdTask = await _taskService.CreateTaskAsync(taskDto);
            return CreatedAtAction("GetTask", new { id = createdTask.Id }, createdTask);
        }

        // PUT: api/tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTarefaDto taskDto)
        {
            var task = await _taskService.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            // Verifica se o usuário é administrador ou se é o responsável pela tarefa
            if (IsAdminOrTaskOwner(task))
            {
                await _taskService.UpdateTaskAsync(id, taskDto);
                return NoContent();
            }
            else
            {
                return Forbid();
            }
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            // Verifica se o usuário é administrador ou se é o responsável pela tarefa
            if (IsAdminOrTaskOwner(task))
            {
                await _taskService.DeleteTaskAsync(id);
                return NoContent();
            }
            else
            {
                return Forbid();
            }
        }

        // Função auxiliar para obter o ID do usuário logado
        private string GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                throw new Exception("ID de usuário não encontrado.");
            }

            return userIdClaim;
        }

        // Função auxiliar para verificar se o usuário é administrador ou o dono da tarefa
        private bool IsAdminOrTaskOwner(Tarefa task)
        {
            var isAdmin = HttpContext.User.IsInRole("Admin");
            var userId = GetCurrentUserId();

            return isAdmin || task.UsuarioAssocioadoId == userId.ToString();
        }
    }
}
