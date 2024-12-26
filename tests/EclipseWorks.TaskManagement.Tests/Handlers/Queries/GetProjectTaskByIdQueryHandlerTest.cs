using System.Net;
using AutoFixture;
using EclipseWorks.TaskManagement.Application.Handlers.Queries;
using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using EclipseWorks.TaskManagement.Models;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using Xunit;

namespace EclipseWorks.TaskManagement.Tests.Handlers.Queries;

public class GetProjectTaskByIdQueryHandlerTest
{
    private readonly ITasksRepository _taskRepository;
    private readonly GetProjectTaskByIdQueryHandler _sut;
    private readonly Fixture _fixture = Substitute.For<Fixture>();

    public GetProjectTaskByIdQueryHandlerTest()
    {
        _taskRepository = Substitute.For<ITasksRepository>();
        _sut = new GetProjectTaskByIdQueryHandler(_taskRepository);
    }

    [Fact]
    public async Task Handle_WithExistingProject_ReturnsSuccessResponse()
    {
        //Arrange
        var request = _fixture.Create<GetProjectTaskByIdRequest>();
        var filteredTask = _fixture.Create<ProjectTask>();
        _taskRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(filteredTask);

        //Act
        var response = await _sut.Handle(request, CancellationToken.None);

        //Assert
        response.ShouldNotBeNull().Resource.ShouldNotBeNull().ShouldBeOfType<ProjectTask>();
    }
    
    [Fact]
    public async Task Handle_WithNonExistingProject_ReturnsFailure()
    {
        //Arrange
        var request = _fixture.Create<GetProjectTaskByIdRequest>();
        _taskRepository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

        //Act
        var response = await _sut.Handle(request, CancellationToken.None);

        //Assert
        response.Resource.ShouldBeNull();
        response.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}