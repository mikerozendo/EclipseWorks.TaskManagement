using System.Net;
using AutoFixture;
using EclipseWorks.TaskManagement.Application.Handlers.Commands;
using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using EclipseWorks.TaskManagement.Models;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using Xunit;

namespace EclipseWorks.TaskManagement.Tests.Handlers.Commands;

public sealed class UpdateTaskStatusHandlerTest
{
    private readonly UpdateTaskStatusHandler _sut;
    private readonly ITasksRepository _tasksRepository;
    private readonly ITasksHistoryRepository _tasksHistoryRepository;
    private readonly Fixture _fixture;

    public UpdateTaskStatusHandlerTest()
    {
        _fixture = Substitute.For<Fixture>();
        _tasksRepository = Substitute.For<ITasksRepository>();
        _tasksHistoryRepository = Substitute.For<ITasksHistoryRepository>();
        _sut = new UpdateTaskStatusHandler(_tasksRepository, _tasksHistoryRepository);
    }

    [Fact]
    public async Task Handle_WithNonExistingTask_ShouldReturnsFailure()
    {
        //Arrange
        var request = _fixture.Create<UpdateTaskStatusRequest>();
        _tasksRepository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert
        result.ShouldBeOfType<ResourceCommandOnErrorResponse>().HttpStatusCode
            .ShouldBe(HttpStatusCode.UnprocessableEntity);
    }

    [Theory]
    [InlineData(ProjectTaskStatus.Pending)]
    [InlineData(ProjectTaskStatus.Doing)]
    [InlineData(ProjectTaskStatus.Done)]
    public async Task Handle_WithValidExistingTask_ShouldReturnsSuccess(ProjectTaskStatus status)
    {
        //Arrange
        var request = _fixture.Build<UpdateTaskStatusRequest>()
            .With(x => x.ProjectTaskStatus, status)
            .Create();
        
        var filteredTask = _fixture.Create<ProjectTask>();

        _tasksRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(filteredTask);
        _tasksHistoryRepository.CreateAsync(Arg.Any<TaskHistory>()).Returns(Task.CompletedTask);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert
        result.ShouldBeOfType<ResourceCommandOnSuccessResponse>();
    }
}