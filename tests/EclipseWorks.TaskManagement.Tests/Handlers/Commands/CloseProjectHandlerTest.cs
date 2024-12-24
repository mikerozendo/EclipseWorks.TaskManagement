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

public sealed class CloseProjectHandlerTest
{
    private readonly CloseProjectHandler _sut;
    private readonly ITasksRepository _tasksRepository;
    private readonly IProjectsRepository _projectsRepository;
    private readonly Fixture _fixture;

    public CloseProjectHandlerTest()
    {
        _fixture = Substitute.For<Fixture>();
        _tasksRepository = Substitute.For<ITasksRepository>();
        _projectsRepository = Substitute.For<IProjectsRepository>();
        _sut = new CloseProjectHandler(_tasksRepository, _projectsRepository);
    }

    [Fact]
    public async Task Handle_WithOnlyCompletedTasks_ReturnsSuccess()
    {
        //Arrange

        var request = _fixture.Create<CloseProjectByIdRequest>();
        var project = _fixture.Create<Project>();

        var completedTasks = _fixture.Build<ProjectTask>()
            .With(x => x.Status, ProjectTaskStatus.Done)
            .CreateMany(5)
            .ToList();

        _projectsRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(project);
        _tasksRepository.GetByProjectIdAsync(Arg.Any<Guid>()).Returns(completedTasks);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert
        result.ShouldBeOfType<ResourceCommandOnSuccessResponse>();
    }
    
    [Theory]
    [InlineData(ProjectTaskStatus.Pending)]
    [InlineData(ProjectTaskStatus.Doing)]
    public async Task Handle_WithOnlyUncompletedTasks_ReturnsFailure(ProjectTaskStatus projectTaskStatus)
    {
        //Arrange

        var request = _fixture.Create<CloseProjectByIdRequest>();
        var project = _fixture.Create<Project>();

        var uncompletedTasks = _fixture.Build<ProjectTask>()
            .With(x => x.Status, projectTaskStatus)
            .CreateMany(5)
            .ToList();

        _projectsRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(project);
        _tasksRepository.GetByProjectIdAsync(Arg.Any<Guid>()).Returns(uncompletedTasks);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert
        result.ShouldBeOfType<ResourceCommandOnErrorResponse>();
    }
    
    [Fact]
    public async Task Handle_WithOnlyOneCompletedTask_ReturnsFailure()
    {
        //Arrange

        var request = _fixture.Create<CloseProjectByIdRequest>();
        var project = _fixture.Create<Project>();

        var completedTask = _fixture.Build<ProjectTask>()
            .With(x => x.Status, ProjectTaskStatus.Done)
            .Create();
        
        var tasks = _fixture.Build<ProjectTask>()
            .With(x => x.Status, ProjectTaskStatus.Pending)
            .CreateMany(5)
            .ToList();

        tasks.Add(completedTask);
        
        _projectsRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(project);
        _tasksRepository.GetByProjectIdAsync(Arg.Any<Guid>()).Returns(tasks);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert
        result.ShouldBeOfType<ResourceCommandOnErrorResponse>();
    }
}