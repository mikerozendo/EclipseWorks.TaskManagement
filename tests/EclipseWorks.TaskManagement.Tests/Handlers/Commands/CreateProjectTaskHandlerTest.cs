using System.Net;
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

public sealed class CreateProjectTaskHandlerTest
{
    private readonly CreateProjectTaskHandler _sut;
    private readonly ITasksRepository _tasksRepository;
    private readonly IProjectsRepository _projectsRepository;
    private readonly Fixture _fixture;

    public CreateProjectTaskHandlerTest()
    {
        _fixture = Substitute.For<Fixture>();
        _projectsRepository = Substitute.For<IProjectsRepository>();
        _tasksRepository = Substitute.For<ITasksRepository>();
        _sut = new CreateProjectTaskHandler(_projectsRepository, _tasksRepository);
    }

    [Fact]
    public async Task Handle_WithNullProject_ReturnsHttpStatusCodeAsUnprocessableEntity()
    {
        //Arrange
        Project? project = null;
        _projectsRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(project);

        var request = _fixture.Create<CreateProjectTaskRequest>();

        //Act
        var handlerResponse = await _sut.Handle(request, CancellationToken.None);

        //Assert
        handlerResponse.ShouldBeAssignableTo<ResourceCommandOnErrorResponse>()?
            .HttpStatusCode.ShouldBe(HttpStatusCode.UnprocessableEntity);
    }

    [Fact]
    public async Task Handle_WithMaxTasksCount_ReturnsHttpStatusCodeAsUnprocessableEntity()
    {
        //Arrange
        var projectTasks = _fixture.CreateMany<ProjectTask>(20).ToList();
        var projectTasksIds = projectTasks.Select(x => x.Id).ToList();
        
        var filteredProject = _fixture.Build<Project>()
            .With(x => x.TaskIds, projectTasksIds)
            .Create();

        _projectsRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(filteredProject);
        
        var request = _fixture.Create<CreateProjectTaskRequest>();

        //Act
        var handlerResponse = await _sut.Handle(request, CancellationToken.None);

        //Assert
        handlerResponse.ShouldBeAssignableTo<ResourceCommandOnErrorResponse>()?
            .HttpStatusCode.ShouldBe(HttpStatusCode.UnprocessableEntity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    [InlineData(11)]
    [InlineData(12)]
    [InlineData(13)]
    [InlineData(14)]
    [InlineData(15)]
    [InlineData(16)]
    [InlineData(17)]
    [InlineData(18)]
    [InlineData(19)]
    public async Task Handle_WithValidScenario_ReturnsSuccess(int tasksCount)
    {
        //Arrange
        var projectTasks = _fixture.CreateMany<ProjectTask>(tasksCount)
            .ToList();
        
        var projectTasksIds = projectTasks
            .Select(x => x.Id)
            .ToList();
        

        var filteredProject = _fixture.Build<Project>()
            .With(x => x.TaskIds, projectTasksIds)
            .Create();

        _projectsRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(filteredProject);

        var request = _fixture.Create<CreateProjectTaskRequest>();
        _projectsRepository.UpdateAsync(Arg.Any<Project>()).Returns(Task.CompletedTask);
        _tasksRepository.CreateAsync(Arg.Any<ProjectTask>()).Returns(Task.CompletedTask);

        //Act
        var handlerResponse = await _sut.Handle(request, CancellationToken.None);

        //Assert
        handlerResponse.ShouldBeAssignableTo<ResourceCommandOnSuccessResponse>();
    }
}