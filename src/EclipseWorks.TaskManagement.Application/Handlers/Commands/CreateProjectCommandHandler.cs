using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using EclipseWorks.TaskManagement.Models;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Handlers.Commands;

public sealed class CreateProjectCommandHandler(IProjectsRepository repository)
    : IRequestHandler<CreateProjectRequest, IResourceCommandResponse>
{
    public async Task<IResourceCommandResponse> Handle(CreateProjectRequest request,
        CancellationToken cancellationToken)
    {
        var project = new Project
        {
            Id = request.Id,
            CreatedAt = DateTime.UtcNow,
            ProjectStatus = ProjectStatus.Created,
            TaskIds = []
        };

        await repository.CreateAsync(project);

        return new ResourceCommandOnSuccessResponse()
        {
            Resource = project.Id
        };
    }
}