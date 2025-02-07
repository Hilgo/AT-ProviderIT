﻿using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GerenciamentoTarefasApi.Data.Dtos;
using GerenciamentoTarefasApi.Models;
using GerenciamentoTarefasApi.Data;

namespace GerenciamentoTarefasApi.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TarefaService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Tarefa>> GetAllTasksAsync(string userId = null, StatusTarefa? status = null)
        {
            // Consulta base
            var query = _context.Tarefas.AsQueryable();

            // Filtrar por usuário (se fornecido)
            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(t => t.UsuarioAssocioadoId == userId);
            }

            // Filtrar por status (se fornecido)
            if (status.HasValue)
            {
                query = query.Where(t => t.Status == status.Value);
            }

            return await query.Include(t => t.UsuarioAssocioado).ToListAsync();
        }

        public async Task<Tarefa> GetTaskByIdAsync(int id)
        {
            return await _context.Tarefas.Include(t => t.UsuarioAssocioado).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tarefa> CreateTaskAsync(CreateTarefaDto taskDto)
        {
            var task = _mapper.Map<Tarefa>(taskDto);

            // Define o status inicial da tarefa como "Pendente"
            task.Status = StatusTarefa.Pendente;

            _context.Tarefas.Add(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<Tarefa> UpdateTaskAsync(int id, UpdateTarefaDto taskDto)
        {
            var existingTask = await _context.Tarefas.FindAsync(id);

            if (existingTask == null)
            {
                throw new Exception("Tarefa não encontrada.");
            }

            // Atualiza apenas os campos permitidos
            existingTask.Titulo = taskDto.Titulo;
            existingTask.Descricao = taskDto.Descricao;
            existingTask.DataVencimento = taskDto.DataVencimento;
            existingTask.Status = taskDto.Status;

            await _context.SaveChangesAsync();

            return existingTask;
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tarefas.FindAsync(id);

            if (task == null)
            {
                throw new Exception("Tarefa não encontrada.");
            }

            _context.Tarefas.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}

