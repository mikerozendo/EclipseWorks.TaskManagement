using AutoFixture;
using EclipseWorks.TaskManagement.Application.Handlers.Commands;
using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using EclipseWorks.TaskManagement.Models;
using NSubstitute;
using Shouldly;
using Xunit;

namespace EclipseWorks.TaskManagement.Tests.Handlers.Commands;

public sealed class DeleteTaskByIdHandlerTest
{
    private readonly DeleteTaskByIdHandler _sut;
    private readonly ITasksRepository _tasksRepository;
    private readonly IProjectsRepository _projectsRepository;
    private readonly Fixture _fixture;

    public DeleteTaskByIdHandlerTest()
    {
        _fixture = Substitute.For<Fixture>();
        _tasksRepository = Substitute.For<ITasksRepository>();
        _projectsRepository = Substitute.For<IProjectsRepository>();
        _sut = new DeleteTaskByIdHandler(_projectsRepository, _tasksRepository);
    }

    [Fact]
    public async Task Handle_WithNonExistingTask_ReturnsFailure()
    {
        //Arrange
        var request = _fixture.Create<DeleteTaskByIdRequest>();
        var filteredTask = _fixture.Create<ProjectTask>();
        var project = _fixture.Create<Project>();

        _tasksRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(filteredTask);
        _projectsRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(project);
        
        _tasksRepository.DeleteByIdAsync(filteredTask.Id).Returns(Task.CompletedTask);
        _projectsRepository.UpdateAsync(project).Returns(Task.CompletedTask);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert
        result.ShouldBeOfType<ResourceCommandOnSuccessResponse>();
    }
}