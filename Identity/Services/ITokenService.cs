using GerenciamentoTarefasApi.Models;
using UsuariosApi.Models;

namespace GerenciamentoTarefasApi.Services
{
    public interface ITokenService
    {
        public string GenerateToken(Usuario usuario, string role);
    }
}
