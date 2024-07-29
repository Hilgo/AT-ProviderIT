using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UsuariosApi.Models;

namespace GerenciamentoTarefasApi.Models
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Titulo { get; set; }

        [MaxLength(2000)]
        public string Descricao { get; set; }

        [Required]
        public DateTime DataVencimento { get; set; }

        [Required]
        public StatusTarefa Status { get; set; }

        // Chave estrangeira para o usuário responsável
        [ForeignKey("UsuarioAssocioado")]
        public string UsuarioAssocioadoId { get; set; }

        // Propriedade de navegação para o usuário
        public Usuario UsuarioAssocioado { get; set; }
    }

    public enum StatusTarefa
    {
        Pendente,
        EmProgresso,
        Concluido
    }
}
