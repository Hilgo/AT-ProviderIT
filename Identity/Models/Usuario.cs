using GerenciamentoTarefasApi.Models;
using Microsoft.AspNetCore.Identity;

namespace UsuariosApi.Models
{
    public class Usuario : IdentityUser
    {
        public Usuario() : base() { }

        public List<Tarefa> Tasks { get; set; } = new List<Tarefa>();
    }
}
