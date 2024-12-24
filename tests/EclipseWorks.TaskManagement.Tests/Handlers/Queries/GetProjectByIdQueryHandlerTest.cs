using AutoFixture;
using EclipseWorks.TaskManagement.Application.Handlers.Queries;
using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using EclipseWorks.TaskManagement.Models;
using NSubstitute;
using Shouldly;
using Xunit;

namespace EclipseWorks.TaskManagement.Tests.Handlers.Queries;

public sealed class GetProjectByIdQueryHandlerTest
{
    private readonly IProjectsRepository _projectsRepository;
    private readonly GetProjectByIdQueryHandler _sut;
    private readonly Fixture _fixture = Substitute.For<Fixture>();

    public GetProjectByIdQueryHandlerTest()
    {
        _projectsRepository = Substitute.For<IProjectsRepository>();
        _sut = new GetProjectByIdQueryHandler(_projectsRepository);
    }

    [Fact]
    public async Task Handle_WithValidDate_ReturnsSuccessResponse()
    {
        //Arrange
        var request = _fixture.Create<GetProjectByIdQueryRequest>();
        var filteredTask = _fixture.Create<Project>();
        _projectsRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(filteredTask);

        //Act
        var response = await _sut.Handle(request, CancellationToken.None);

        //Assert
        response.ShouldNotBeNull().Resource.ShouldNotBeNull().ShouldBeOfType<Project>();
    }
}