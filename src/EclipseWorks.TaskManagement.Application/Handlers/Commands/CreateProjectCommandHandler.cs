using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Handlers.Commands;

public sealed class CreateProjectCommandHandler
    : IRequestHandler<CreateProjectRequest, IResourceCommandResponse>
{
    public async Task<IResourceCommandResponse> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return new ResourceCommandOnSuccessResponse()
        {
            ResourceId = Guid.NewGuid()
        };
    }
}