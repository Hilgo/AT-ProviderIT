using UsuariosApi.Data.Dtos;

namespace UsuariosApi.Services
{
    public interface ICadastroService
    {
        Task Cadastra(CreateUsuarioDto dto);
    }
}