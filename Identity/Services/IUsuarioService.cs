using UsuariosApi.Data.Dtos;

namespace GerenciamentoTarefasApi.Services
{
    public interface IUsuarioService
    {
        Task<string> Login(LoginUsuarioDto dto);
    }
}
