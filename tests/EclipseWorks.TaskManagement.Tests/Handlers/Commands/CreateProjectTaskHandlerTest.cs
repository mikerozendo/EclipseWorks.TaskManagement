using System.Net;
using AutoFixture;
using EclipseWorks.TaskManagement.Application.Handlers.Commands;
using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure.Repositories;
using EclipseWorks.TaskManagement.Models;
using NSubstitute;
using Shouldly;
using Xunit;

namespace EclipseWorks.TaskManagement.Tests.Handlers.Commands;

public sealed class CreateProjectTaskHandlerTest
{
    private readonly CreateProjectTaskHandler _sut;
    private readonly IRepository<Project> _repository;
    private readonly IRepository<ProjectHistory> _projectsHistoryRepository;
    private readonly IProjectsRepository _projectsRepository;
    private readonly Fixture _fixture;

    public CreateProjectTaskHandlerTest()
    {
        _fixture = Substitute.For<Fixture>();
        _repository = Substitute.For<IRepository<Project>>();
        _projectsRepository = Substitute.For<IProjectsRepository>();
        _projectsHistoryRepository = Substitute.For<IRepository<ProjectHistory>>();
        _sut = new CreateProjectTaskHandler(_repository, _projectsHistoryRepository, _projectsRepository);
    }

    [Fact]
    public async Task Handle_WithNullProject_ReturnsHttpStatusCodeAsUnprocessableEntity()
    {
        //Arrange
        Project? project = null;
        _repository.GetByIdAsync(Arg.Any<Guid>()).Returns(project);

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
        var filteredProject = _fixture.Build<Project>()
            .With(x => x.Tasks, projectTasks)
            .Create();

        _repository.GetByIdAsync(Arg.Any<Guid>()).Returns(filteredProject);

        var request = _fixture.Create<CreateProjectTaskRequest>();

        //Act
        var handlerResponse = await _sut.Handle(request, CancellationToken.None);

        //Assert
        handlerResponse.ShouldBeAssignableTo<ResourceCommandOnErrorResponse>()?
            .HttpStatusCode.ShouldBe(HttpStatusCode.UnprocessableEntity);
    }
}