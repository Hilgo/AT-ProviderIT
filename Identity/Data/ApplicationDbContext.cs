﻿using GerenciamentoTarefasApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Models;

namespace UsuariosApi.Data
{
    public class ApplicationDbContext :
    IdentityDbContext<Usuario>
    {
        public ApplicationDbContext
            (DbContextOptions<ApplicationDbContext> opts) : base(opts) { }

        public DbSet<Tarefa> Tasks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar o relacionamento entre Tarefa e Usuário
            modelBuilder.Entity<Tarefa>()
                .HasOne(t => t.UsuarioAssocioado) // Uma tarefa pertence a um usuário
                .WithMany(u => u.Tasks)       // Um usuário pode ter várias tarefas
                .HasForeignKey(t => t.UsuarioAssocioadoId) // Especifica a chave estrangeira
                .OnDelete(DeleteBehavior.Restrict); // Define o comportamento ao excluir um usuário (opcional - pode ser Cascade, SetNull, etc.)
        }
    }
}
