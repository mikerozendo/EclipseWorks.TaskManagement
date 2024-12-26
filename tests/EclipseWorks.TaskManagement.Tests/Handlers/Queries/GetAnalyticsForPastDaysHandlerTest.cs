using System.Net;
using AutoFixture;
using EclipseWorks.TaskManagement.Application.Handlers.Queries;
using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using EclipseWorks.TaskManagement.Models;
using MongoDB.Driver.Linq;
using NSubstitute;
using Shouldly;
using Xunit;

namespace EclipseWorks.TaskManagement.Tests.Handlers.Queries;

public sealed class GetAnalyticsForPastDaysHandlerTest
{
    private readonly ITasksRepository _tasksRepository;
    private readonly GetAnalyticsForPastDaysHandler _sut;
    private readonly Fixture _fixture = Substitute.For<Fixture>();

    public GetAnalyticsForPastDaysHandlerTest()
    {
        _tasksRepository = Substitute.For<ITasksRepository>();
        _sut = new GetAnalyticsForPastDaysHandler(_tasksRepository);
    }

    [Fact]
    public async Task Handle_WithEmptyResult_ReturnsEmptyCollection()
    {
        //Arrange 
        var request = _fixture.Create<GetAnalyticsForPastDaysRequest>();

        _tasksRepository.GetClosedTasksByPeriodAsync(Arg.Any<DateTime>()).Returns([]);

        //Act
        var response = await _sut.Handle(request, CancellationToken.None);

        //Assert
        response.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        response.Success.ShouldBeFalse();
    }
    
    [Fact]
    public async Task Handle_WithValidResult_ReturnsSuccess()
    {
        //Arrange 
        var request = _fixture.Create<GetAnalyticsForPastDaysRequest>();

        var tasks = _fixture.Build<ProjectTask>()
            .With(x => x.Status, ProjectTaskStatus.Done)
            .With(x => x.ClosedAt, DateTime.UtcNow.AddDays(-30))
            .CreateMany(10)
            .ToList();
            
        _tasksRepository.GetClosedTasksByPeriodAsync(Arg.Any<DateTime>()).Returns(tasks);

        //Act
        var response = await _sut.Handle(request, CancellationToken.None);

        //Assert
        response.HttpStatusCode.ShouldBe(HttpStatusCode.OK);
        response.Resource.ShouldBeOfType<List<GetAnalyticsForPastDaysResponse>>().Count.ShouldBeGreaterThan(0);
        response.Success.ShouldBeTrue();
    }
}