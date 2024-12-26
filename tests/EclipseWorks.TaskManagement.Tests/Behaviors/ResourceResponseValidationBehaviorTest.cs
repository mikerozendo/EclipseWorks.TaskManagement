using System.Net;
using AutoFixture;
using EclipseWorks.TaskManagement.Application.Behaviors;
using EclipseWorks.TaskManagement.Application.Exceptions;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Application.Responses.Interfaces;
using MediatR;
using NSubstitute;
using Shouldly;
using Xunit;

namespace EclipseWorks.TaskManagement.Tests.Behaviors;

public sealed class ResourceResponseValidationBehaviorTest
{
    private readonly Fixture _fixture = Substitute.For<Fixture>();

    private readonly RequestHandlerDelegate<IResourceResponse> _requestHandlerDelegate =
        Substitute.For<RequestHandlerDelegate<IResourceResponse>>();

    [Fact]
    public async Task Handle_WithInvalidResponseConfiguration_ShouldThrowsInvalidResponseConfigurationException()
    {
        // Arrange
        var request = Substitute.For<IRequest<IResourceResponse>>();

        var invalidResponse = _fixture.Build<ResourceCommandOnSuccessResponse>()
            .With(r => r.HttpStatusCode, HttpStatusCode.OK)
            .Without(r => r.Resource)
            .Create();

        _requestHandlerDelegate().Returns(invalidResponse);

        var behavior = new ResourceResponseValidationBehavior<IRequest<IResourceResponse>, IResourceResponse>();

        // Act & Assert
        await Should.ThrowAsync<InvalidResponseConfigurationException>(() =>
            behavior.Handle(request, _requestHandlerDelegate, CancellationToken.None)
        );
    }
    
    [Fact]
    public async Task Handle_WithValidResponseConfiguration_ShouldThrowsInvalidResponseConfigurationException()
    {
        // Arrange
        var request = Substitute.For<IRequest<IResourceResponse>>();

        var invalidResponse = _fixture.Build<ResourceCommandOnSuccessResponse>()
            .With(r => r.HttpStatusCode, HttpStatusCode.OK)
            .With(r => r.Resource, Guid.NewGuid())
            .Create();

        _requestHandlerDelegate().Returns(invalidResponse);

        var behavior = new ResourceResponseValidationBehavior<IRequest<IResourceResponse>, IResourceResponse>();

        //Act
        var response = await behavior.Handle(request, _requestHandlerDelegate, CancellationToken.None);
        
        //Assert
        response.ShouldNotBeNull();
    }
}