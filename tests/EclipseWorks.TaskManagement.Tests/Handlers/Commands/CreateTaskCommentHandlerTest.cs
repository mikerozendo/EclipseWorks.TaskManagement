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

public sealed class CreateTaskCommentHandlerTest
{
    private readonly CreateTaskCommentHandler _sut;
    private readonly ITasksRepository _tasksRepository;
    private readonly ITasksHistoryRepository _tasksHistoryRepository;
    private readonly Fixture _fixture;

    public CreateTaskCommentHandlerTest()
    {
        _fixture = Substitute.For<Fixture>();
        _tasksRepository = Substitute.For<ITasksRepository>();
        _tasksHistoryRepository = Substitute.For<ITasksHistoryRepository>();
        _sut = new CreateTaskCommentHandler(_tasksRepository, _tasksHistoryRepository);
    }

    [Fact]
    public async Task Handle_WithNonExistingTask_ShouldReturnsFailure()
    {
        //Arrange
        var request = _fixture.Create<CreateTaskCommentRequest>();
        
        _tasksRepository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

        //Act
        var response = await _sut.Handle(request, CancellationToken.None);

        //Assert
        response.ShouldBeOfType<ResourceCommandOnErrorResponse>().HttpStatusCode
            .ShouldBe(HttpStatusCode.UnprocessableEntity);
    }

    [Fact]
    public async Task Handle_WithValidExistingTask_ShouldReturnsSuccess()
    {
        //Arrange
        var request = _fixture.Create<CreateTaskCommentRequest>();
        var filteredTask = _fixture.Create<ProjectTask>();
        
        _tasksRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(filteredTask);
        _tasksHistoryRepository.CreateAsync(Arg.Any<TaskHistory>()).Returns(Task.CompletedTask);
        
        //Act
        var response = await _sut.Handle(request, CancellationToken.None);

        //Assert
        response.ShouldBeOfType<ResourceCommandOnSuccessResponse>();
    }
}