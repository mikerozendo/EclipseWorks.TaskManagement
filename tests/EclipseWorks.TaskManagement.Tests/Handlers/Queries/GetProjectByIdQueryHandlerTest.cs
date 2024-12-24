using AutoFixture;
using EclipseWorks.TaskManagement.Application.Handlers.Queries;
using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using EclipseWorks.TaskManagement.Models;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using Xunit;

namespace EclipseWorks.TaskManagement.Tests.Handlers.Queries;

public sealed class GetProjectByIdQueryHandlerTest
{
    private readonly IProjectsRepository _projectsRepository;
    private readonly ITasksRepository _tasksRepository;
    private readonly GetProjectByIdQueryHandler _sut;
    private readonly Fixture _fixture = Substitute.For<Fixture>();

    public GetProjectByIdQueryHandlerTest()
    {
        _projectsRepository = Substitute.For<IProjectsRepository>();
        _tasksRepository = Substitute.For<ITasksRepository>();
        _sut = new GetProjectByIdQueryHandler(_projectsRepository, _tasksRepository);
    }
    
    [Fact]
    public async Task Handle_WithNonExistingProject_ReturnsFailure()
    {
        //Arrange
        var request = _fixture.Create<GetProjectByIdQueryRequest>();
        _projectsRepository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

        //Act
        var response = await _sut.Handle(request, CancellationToken.None);

        //Assert
        response.ShouldBeOfType<ResourceQueryResponse>().Resource.ShouldBeNull();
    }

    [Fact]
    public async Task Handle_WithValidProject_ReturnsSuccessResponse()
    {
        //Arrange
        var request = _fixture.Create<GetProjectByIdQueryRequest>();
        var filteredTask = _fixture.Create<Project>();
        _projectsRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(filteredTask);
        _tasksRepository.GetByProjectIdAsync(Arg.Any<Guid>()).Returns([]);

        //Act
        var response = await _sut.Handle(request, CancellationToken.None);

        //Assert
        response.ShouldNotBeNull().Resource.ShouldNotBeNull().ShouldBeOfType<Project>();
    }
}