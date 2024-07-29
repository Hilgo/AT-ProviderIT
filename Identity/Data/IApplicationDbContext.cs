using GerenciamentoTarefasApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoTarefasApi.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Tarefa> Tarefas { get; set; }
        // Adicione outros DbSets para outras entidades, se necessário

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
