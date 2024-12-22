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

public sealed class CreateProjectCommandHandlerTest
{
    private readonly IProjectsRepository _projectsRepository;
    private readonly CreateProjectCommandHandler _sut;
    private readonly Fixture _fixture = Substitute.For<Fixture>();

    public CreateProjectCommandHandlerTest()
    {
        _projectsRepository = Substitute.For<IProjectsRepository>();
        _sut = new CreateProjectCommandHandler(_projectsRepository);
    }

    [Fact]
    public async Task Handle_WithValidDate_ReturnsSuccessResponse()
    {
        //Arrange
        var request = _fixture.Create<CreateProjectRequest>();
        _projectsRepository.InsertAsync(Arg.Any<Project>()).Returns(Task.CompletedTask);

        //Act
        var response = await _sut.Handle(request, CancellationToken.None);

        //Assert

        response.ShouldBeOfType<ResourceCommandOnSuccessResponse>();
    }
}