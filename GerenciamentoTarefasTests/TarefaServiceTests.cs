using AutoMapper;
using Moq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using GerenciamentoTarefasApi.Data.Dtos;
using GerenciamentoTarefasApi.Data;
using GerenciamentoTarefasApi.Models;
using GerenciamentoTarefasApi.Services;

namespace GerenciamentoTarefasTests;
public class TarefaServiceTests
{
    private readonly Mock<IApplicationDbContext> _mockContext;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ITarefaService _tarefaService;

    public TarefaServiceTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();
        _mockMapper = new Mock<IMapper>();
        _tarefaService = new TarefaService(_mockContext.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task CriaTarefaAsync_DeveGerarTask()
    {
        // Arrange
        var taskDto = new CreateTarefaDto { Titulo = "New Task", Descricao = "Description", DataVencimento = DateTime.Now };
        var task = new Tarefa { Id = 1, Titulo = "New Task", Descricao = "Description", DataVencimento = DateTime.Now, Status = StatusTarefa.Pendente };

        _mockMapper.Setup(m => m.Map<Tarefa>(taskDto)).Returns(task);
        _mockContext.Setup(c => c.Tarefas.Add(task));
        _mockContext.Setup(c => c.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        // Act
        var result = await _tarefaService.CreateTaskAsync(taskDto);

        // Assert
        result.Should().BeEquivalentTo(task);
        _mockContext.Verify(c => c.Tarefas.Add(It.IsAny<Tarefa>()), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task AlteraTarefaAsync_DeveAlterarTarefaQuandoExiste()
    {
        // Arrange
        var existingTask = new Tarefa { Id = 1, Titulo = "Existing Task", Descricao = "Existing Description", DataVencimento = DateTime.Now, Status = StatusTarefa.Pendente };
        var taskDto = new UpdateTarefaDto { Titulo = "Updated Task", Descricao = "Updated Description", DataVencimento = DateTime.Now, Status = StatusTarefa.Concluido };

        _mockContext.Setup(c => c.Tarefas.FindAsync(1)).ReturnsAsync(existingTask);
        _mockContext.Setup(c => c.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        // Act
        var result = await _tarefaService.UpdateTaskAsync(1, taskDto);

        // Assert
        result.Titulo.Should().Be(taskDto.Titulo);
        result.Descricao.Should().Be(taskDto.Descricao);
        result.Status.Should().Be(taskDto.Status);
        _mockContext.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task ExcluiTarefaAsync_DeveRemoverTarefaQuandoExistir()
    {
        // Arrange
        var task = new Tarefa { Id = 1, Titulo = "Task to be deleted" };

        _mockContext.Setup(c => c.Tarefas.FindAsync(1)).ReturnsAsync(task);
        _mockContext.Setup(c => c.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        // Act
        await _tarefaService.DeleteTaskAsync(1);

        // Assert
        _mockContext.Verify(c => c.Tarefas.Remove(task), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

  
}
