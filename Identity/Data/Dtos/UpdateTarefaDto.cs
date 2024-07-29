using GerenciamentoTarefasApi.Models;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoTarefasApi.Data.Dtos
{
    public class UpdateTarefaDto
    {

        [Required]
        [MaxLength(255)]
        public string Titulo { get; set; }

        [MaxLength(2000)]
        public string Descricao { get; set; }

        [Required]
        public DateTime DataVencimento { get; set; }

        [Required]
        public StatusTarefa Status { get; set; }
    }
}

